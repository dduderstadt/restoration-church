using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace rcliberty.Web.Models
{
    public class PodcastModels
    {
        public class Podcast
        {
            public string WrapperType { get; set; }
            public string Kind { get; set; }
            public int CollectionId { get; set; }
            public int TrackId { get; set; }
            public string ArtistName { get; set; }
            public string CollectionName { get; set; }
            public string TrackName { get; set; }
            public string CollectionCensoredName { get; set; }
            public string TrackCensoredName { get; set; }
            public string CollectionViewUrl { get; set; }
            public string FeedUrl { get; set; }
            public string TrackViewUrl { get; set; }
            public string ArtworkUrl30 { get; set; }
            public string ArtworkUrl60 { get; set; }
            public string ArtworkUrl100 { get; set; }
            public double CollectionPrice { get; set; }
            public double TrackPrice { get; set; }
            public int TrackRentalPrice { get; set; }
            public int TrackHdPrice { get; set; }
            public int CollectionHdPrice { get; set; }
            public string ReleaseDate { get; set; }
            public string CollectionExplicitness { get; set; }
            public string TrackExplicitness { get; set; }
            public int TrackCount { get; set; }
            public string Country { get; set; }
            public string Currency { get; set; }
            public string PrimaryGenreName { get; set; }
            public string ContentAdvisoryRating { get; set; }
            public string ArtworkUrl600 { get; set; }
            public List<string> GenreIds { get; set; }
            public List<string> Genres { get; set; }
        }

        public class Episode
        {
            [Display(Name = "Audio")]
            public string Url { get; set; }

            [Display(Name = "Date")]
            public string PublishDate { get; set; }

            public string Title { get; set; }

            public string Id { get; set; }
        }

        public class Series
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Image { get; set; }
        }

        public class Results
        {
            public int ResultCount { get; set; }
            public List<Podcast> results { get; set; }
        }

        public static List<Podcast> SendPodcastRequest()
        {
            //setup iTunes client & request (URL)
            var client = new RestClient("https://itunes.apple.com");
            var request = new RestRequest("search?term=jp+stafford&entity=podcast&media=podcast&country=us");

            //setup request
            request.Method = Method.GET;
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "text/javascript");

            var response = client.Execute(request);
            var result = JsonConvert.DeserializeObject<Results>(response.Content);

            return result.results;
        }

        public static List<Episode> GetPodcastEpisodes()
        {
            //direct connection (testing)
            //http://jpstafford.podbean.com/feed/

            //TODO podcast by series w/logo (testing)
            //https://deow9bq0xqvbj.cloudfront.net/ep-logo/pbblog254166/Red_Ink_Jesus_Logo-FB.jpg

            List<Episode> episodes = new List<Episode>();

            Podcast result = SendPodcastRequest().FirstOrDefault();
            XDocument doc = XDocument.Load(result.FeedUrl);

            //get data to display from XML tags (.m4a files)
            foreach (XElement e in doc.Descendants("item").Where(x => x.Parent.Name == "channel"))
            {
                //create the episode
                Episode model = new Episode();
                model.Url = e.Element("enclosure").Attribute("url").Value;
                model.PublishDate = Convert.ToDateTime(e.Element("pubDate").Value.Substring(0, 16)).ToString("yyyy / MM / dd");
                model.Title = e.Element("title").Value;

                //using XML <guid> as DB table 'Id'
                var guid = e.Element("guid").Value;
                model.Id = guid.Substring(guid.LastIndexOf('-') + 1);

                episodes.Add(model);
                InsertRecords(model);
            }
            return episodes;
        }

        public static void InsertRecords(Episode model)
        {
            CreateSeries(model);
            CreateEpisode(model);
        }

        public static void CreateSeries(Episode model)
        {
            var marker = model.Title.IndexOf('-');
            var series = model.Title.Substring(0, marker - 1);

            using (SqlConnection conn = new SqlConnection("Data Source =.\\sqlexpress; Initial Catalog = rcliberty; Integrated Security = True"))
            {
                conn.Open();

                SqlCommand getAllSeries = new SqlCommand("SELECT Name FROM Series WHERE Name = @name", conn);
                getAllSeries.Parameters.AddWithValue("@name", series);

                SqlDataReader reader = getAllSeries.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close();

                    string insertSeries = "INSERT INTO Series ([Name]) VALUES(@name)";
                    using (SqlCommand cmd = new SqlCommand(insertSeries, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", series);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void CreateEpisode(Episode model)
        {
            using (SqlConnection conn = new SqlConnection("Data Source =.\\sqlexpress; Initial Catalog = rcliberty; Integrated Security = True"))
            {
                conn.Open();

                int location = model.Title.IndexOf("-");
                string episodeTitle = model.Title.Substring(location + 2);
                string series = model.Title.Substring(0, location - 1);

                SqlCommand getAllEpisodes = new SqlCommand("SELECT Title FROM Episodes WHERE Title = @title", conn);
                getAllEpisodes.Parameters.AddWithValue("@title", episodeTitle);

                SqlDataReader reader = getAllEpisodes.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close();

                    string insertEpisode = "INSERT INTO Episodes ([Id], [Title], [Date], [AudioUrl], [SeriesId]) VALUES(@id, @title, @date, @audioUrl, @seriesId)";
                    using (SqlCommand cmd = new SqlCommand(insertEpisode, conn))
                    {
                        int seriesId = 0;
                        using (SqlCommand getSeriesId = new SqlCommand("SELECT Id FROM Series WHERE Name = @name", conn))
                        {
                            getSeriesId.Parameters.AddWithValue("@name", series);
                            SqlDataReader rdrSeriesId = getSeriesId.ExecuteReader();
                            if (rdrSeriesId.Read())
                            {
                                seriesId = rdrSeriesId.GetInt32(0);
                            }
                            rdrSeriesId.Close();
                        }
                        cmd.Parameters.AddWithValue("@id", model.Id);
                        cmd.Parameters.AddWithValue("@title", episodeTitle);
                        cmd.Parameters.AddWithValue("@date", model.PublishDate);
                        cmd.Parameters.AddWithValue("@audioUrl", model.Url);
                        cmd.Parameters.AddWithValue("@seriesId", seriesId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public static List<Series> GetAllSeries()
        {
            List<Series> seriesList = new List<Series>();

            using (SqlConnection conn = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=rcliberty;Integrated Security=True;"))
            {
                conn.Open();

                SqlCommand cmdGetAllSeries = new SqlCommand("SELECT * FROM Series", conn);
                SqlDataReader rdrSeries = cmdGetAllSeries.ExecuteReader();

                while (rdrSeries.Read())
                {
                    Series s = new Series();
                    s.Id = (int)rdrSeries["Id"];
                    s.Name = (string)rdrSeries["Name"];
                    s.Image = (rdrSeries["Image"] == DBNull.Value) ? string.Empty : (string)rdrSeries["Image"];

                    seriesList.Add(s);
                }
            }

            return seriesList;
        }


        //create MSSQL database (brainstorm)
        //media: podcasts (linking), episodes, categories
        //TODO refactor [above] methods to accept the List<PodcastModel> and store each record in the DB (mapping???)
        //identity: ASP.NET tables
        //admin dashboard: images (file upload)
    }
}