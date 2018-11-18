USE rcliberty
SELECT * FROM Series
--WHERE Name = [Myths & Facts]

SELECT * FROM Episodes
ORDER BY [Date] DESC

SELECT e.Id, Title, [Date], AudioUrl, s.Id
FROM Episodes e
JOIN Series s
ON e.SeriesId = s.Id

SELECT s.Name, COUNT(e.Title) AS 'Series Count'
FROM Series s
JOIN Episodes e
ON e.SeriesId = s.Id
GROUP BY s.Name
ORDER BY 'Series Count' DESC


DELETE FROM Episodes
DELETE FROM Series