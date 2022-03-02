using BBDisplay.Models;

namespace BBDisplay.Classes;

public class SessionsClean : IPDataClean
{

    public List<Session> IndexPrepSessions(List<string> sessData)
    {
        int cL = 0;
        int cA = 0;
        int entries = sessData.Count / 5;

        Session[] viewData = new Session[entries];

        for (int i = 0; i < viewData.Length; i++)
        {
            viewData[i] = new Session();
        }

        while (cA < entries)
        {
            viewData[cA].SessionId = Int32.Parse(sessData[cL]); cL++;
            viewData[cA].UserIPAddress = sessData[cL]; cL++;
            viewData[cA].DateTime = sessData[cL]; cL++;
            viewData[cA].LoggedIn =  sessData[cL]; cL++;
            viewData[cA].PurchaseMade = sessData[cL]; cL++;
            cA++;


        }


        return viewData.ToList();

    }


}

