using BBDisplay.Models;

namespace BBDisplay.Classes;

public class IPDataClean
{
    /// <summary>
    /// Cleans up the data gathered by the api
    /// 
    /// </summary>
    /// <param name="ipAPIData"></param>
    /// <returns></returns>
    public string CleanAPIResponse(string ipAPIData)
    {
        char[] unwantedChars = { '{', '}', '"' };

        foreach (char c in unwantedChars)
        {
            ipAPIData = ipAPIData.Replace(c.ToString(), String.Empty);
        }

        return ipAPIData;

    }


    public string RemoveSquareBraces(string ipAPIData)
    {
        char[] unwantedChars = { '[', ']'};

        foreach (char c in unwantedChars)
        {
            ipAPIData = ipAPIData.Replace(c.ToString(), String.Empty);
        }

        return ipAPIData;


    }


    /// Placed the cleaned data from the API into a string for processing into the database. 
    /// <param name="info"></param>
    /// <returns></returns>
    public List<string> PreppedData(string info)
    {
        string[] ipInfoSplit = info.Split(',');
        string[] ipData = new string[2];
        //string[] prepedData = new string[7];
        List<string> prepedData = new List<string>();
        int count = 0;

        foreach (string item in ipInfoSplit)
        {
            ipData = item.Split(':');
            prepedData.Add(ipData[1].Trim());
            count++;
        }

        return prepedData;

    }


    /// <summary>
    /// This spicey messs increments a count veriable that keeps track of how many entried there should be in the 
    /// ip address table by devided the sum total of all data in the ipData List by 8. Every 8 entried in the string Array 
    /// should be  1 row in the UserIpData table. This information is then placed in a presized array of UserIPData objects for transfer
    /// to the index. 
    /// </summary>
    /// <param name="ipData"></param>
    /// <returns></returns>
    /// 
    
    public List<UserIPData> IndexPrepIPData(List<string> ipData)
    {
        int cL = 0; 
        int cA = 0;
        int ipEnt = ipData.Count / 8;

        UserIPData[] viewData = new UserIPData[ipEnt];

        for (int i = 0; i < viewData.Length; i++)
        {
            viewData[i] = new UserIPData();

        }

        while (cA < ipEnt)
        {
            viewData[cA].UserIP = ipData[cL];
            cL++;

            viewData[cA].CountryCode = ipData[cL];
            cL++;

            viewData[cA].CountryName = ipData[cL];
            cL++;

            viewData[cA].StateOrRegion = ipData[cL];
            cL++;

            viewData[cA].City = ipData[cL];
            cL++;

            viewData[cA].ZipCode = ipData[cL];
            cL++;

            viewData[cA].VisitCount = Int32.Parse(ipData[cL]);
            cL++;

            viewData[cA].DeviceType = ipData[cL];
            cL++;

            cA++;

        }


        return viewData.ToList();
 
    }
}

