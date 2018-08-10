using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public class PodcastDisplayModel
        {
            [Display(Name = "Audio")]
            public string Url { get; set; }

            //[Display(Name = "Date")]
            //[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
            //public DateTime PublishDate { get; set; }

            [Display(Name = "Date")]
            public string PublishDate { get; set; }

            public string Title { get; set; }
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

        public static List<PodcastDisplayModel> GetPodcastEpisodes()
        {
            //direct connection (testing)
            //http://jpstafford.podbean.com/feed/

            //List<string> episodeUrls = new List<string>();
            List<PodcastDisplayModel> episodes = new List<PodcastDisplayModel>();

            Podcast result = SendPodcastRequest().FirstOrDefault();
            XDocument doc = XDocument.Load(result.FeedUrl);

            //get data to display from XML tags (.m4a files)
            foreach (XElement e in doc.Descendants("item").Where(x => x.Parent.Name == "channel"))
            {
                PodcastDisplayModel model = new PodcastDisplayModel();
                model.Url = e.Element("enclosure").Attribute("url").Value;
                //model.PublishDate = DateTime.Parse(e.Element("pubDate").Value.Substring(4, 12));
                model.PublishDate = Convert.ToDateTime(e.Element("pubDate").Value.Substring(0, 16)).ToString("yyyy / MM / dd");
                model.Title = e.Element("title").Value;

                episodes.Add(model);
            }

            //return episodeUrls;
            return episodes;
        }
    }
}