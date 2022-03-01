using BBDisplay.Models;

namespace BBDisplay.Classes
{
    public class PageReferenceDataClean
    {
        /// <summary>
        /// Cleans up the data gathered by the api
        /// 
        /// </summary>
        /// <param name="ipAPIData"></param>
        /// <returns></returns>
        public string CleanAPIResponse(string pageRefAPIData)
        {
            char[] unwantedChars = { '{', '}', '"' };

            foreach (char c in unwantedChars)
            {
                pageRefAPIData = pageRefAPIData.Replace(c.ToString(), String.Empty);
            }

            return pageRefAPIData;

        }


        public string RemoveSquareBraces(string ipAPIData)
        {
            char[] unwantedChars = { '[', ']' };

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
            string[] prInfoSplit = info.Split(',');
            string[] prData = new string[2];
            //string[] prepedData = new string[7];
            List<string> prepedData = new List<string>();
            int count = 0;

            foreach (string item in prInfoSplit)
            {
                prData = item.Split(':');
                prepedData.Add(prData[1].Trim());
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

        public List<PageReference> IndexPrep(List<string> pageRefData)
        {
            int cL = 0;
            int cA = 0;
            int pageEnt = pageRefData.Count;
            pageEnt = pageEnt / 3;
            pageEnt--;

            PageReference[] viewData = new PageReference[pageEnt];

            for (int i = 0; i < viewData.Length; i++)
            {
                viewData[i] = new PageReference();

            }

            while (cA < pageEnt)
            {
                viewData[cA].PageId = Int32.Parse(pageRefData[cL]);
                cL++;

                //viewData[cA].DateAdded = DateTime.Parse(pageRefData[cL]);
                cL++;

                viewData[cA].PageDescription = pageRefData[cL];

                cL++;

                cA++;

            }


            return viewData.ToList();

        }
    }
}
