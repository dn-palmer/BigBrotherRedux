using System.Net;

namespace BigBrotherRedux.Entities;

public class GeoDataCleanUp { 

    /// Makes a call to IP-API to gather geo location data on a particular ip address. 
    /// <param name="ip"></param>
    /// <returns></returns>
    public string GetIPAPIResponse(string ip)
    {
        string url = $"Http://ip-api.com/json/";
        url += $"{ip}?fields=country,countryCode,regionName,city,zip,mobile,query";
        string ipApiResponse;

        WebRequest request = WebRequest.Create(url);

        WebResponse response = request.GetResponse();

        using (Stream dataStream = response.GetResponseStream())
        {
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            ipApiResponse = responseFromServer;
            // Display the content.
            Console.WriteLine(responseFromServer);
        }

        return CleanIPAPIResponse(ipApiResponse);

    }


    /// Cleans the Json data gathered from IP-API and places it into a string array so that  
    /// it can be added to the database.
    /// <param name="ipAPIData"></param>
    /// <returns></returns>
    public string CleanIPAPIResponse(string ipAPIData)
    {
        char[] unwantedChars = { '{', '}', '"' };

        foreach (char c in unwantedChars)
        {
            ipAPIData = ipAPIData.Replace(c.ToString(), String.Empty);
        }

        return ipAPIData;

    }


    /// Placed the cleaned data from IP-API into a string for processing into the database. 
    /// <param name="info"></param>
    /// <returns></returns>
    public string[] DatabaseReadyData(string info)
    {
        string[] ipInfoSplit = info.Split(',');
        string[] ipData = new string[2];
        string[] prepedData = new string[7];
        int count = 0;

        foreach (string item in ipInfoSplit)
        {
            ipData = item.Split(':');
            prepedData[count] = ipData[1].Trim();
            count++;
        }

        return prepedData;

    }
}

