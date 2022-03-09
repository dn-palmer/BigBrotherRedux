using BBDisplay.Models;

namespace BBDisplay.Classes;

public class PageReferenceClean : IPDataClean
{

    public List<PageReference> IndexPrepPageReference(List<string> pageRefData)
    {
        int cL = 0;
        int cA = 0;
        int entries = pageRefData.Count / 3;

        PageReference[] viewData = new PageReference[entries];

        for (int i = 0; i < viewData.Length; i++)
        {
            viewData[i] = new PageReference();
        }

        while (cA < entries)
        {
            viewData[cA].PageId = Int32.Parse(pageRefData[cL]); cL++;
            string sample = "";
            for (int i = 0; i < pageRefData[cL].Length - 3; i++)
            {
                sample += pageRefData[cL][i]; 
            }
            viewData[cA].DateAdded = sample; cL++;
            viewData[cA].PageDescription = pageRefData[cL]; cL++;
            cA++;
        }


        return viewData.ToList();

    }


}

