using BigBrotherRedux.Entities;
using BigBrotherRedux.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigBrotherRedux.Services.Repositories;

public class UserIPDataRepo : IUserIPDataRepo
{
    private readonly BigBrotherReduxContext _db;
 
    /// Grants access to the database.
    /// <param name="db"></param>
    public UserIPDataRepo(BigBrotherReduxContext db)
    {
        _db = db;

    }

    /// Adds a new user to the UserIPData Table.
    /// <param name="ipInfo"></param>
    public void CreateEntry(UserIPData ipInfo)
    {
        _db.UserIPData.Add(ipInfo);
        _db.SaveChanges();
    }
    

    /// Removes the requested data entry from the UserIpData Table.
    /// <param name="ip"></param>
    public void DeleteEntry(string ip)
    {
       var ipData = GetEntry(ip);
        _db.UserIPData.Remove(ipData);
        _db.SaveChanges();
    }

    /// Parses the UserIPData Table for an IP Address and returns a bool value based on what is found. 
    /// <param name="ip"></param>
    public bool EntryExists(string ip)
    {
        var ipList = ReadAll();

        foreach (var i in ipList)
        {
            if (i.UserIP == ip)
            {
                return true;
            }
        }
        return false;

    }

    /// Reads a single entry from the database and returns it.
    /// <param name="ip"></param>
    public UserIPData GetEntry(string ip)
    {
        return _db.UserIPData.FirstOrDefault(i => i.UserIP == ip); 
    }
  
    /// Reads all entries from the UserIPData Table and reurns them in a list.
    public ICollection<UserIPData> ReadAll()
    {
        return _db.UserIPData.AsNoTracking().ToList();
    }
    
    /// Updates an Entry in the UserIPDataTable
    /// <param name="ipInfo"></param>
    public void UpdateEntry(UserIPData ipInfo)
    {
        var ipEntryToUpdate = GetEntry(ipInfo.UserIP);
        ipEntryToUpdate.DeviceType = ipInfo.DeviceType;
        ipEntryToUpdate.CountryCode = ipInfo.CountryCode;
        ipEntryToUpdate.City = ipInfo.City;
        ipEntryToUpdate.StateOrRegion = ipInfo.StateOrRegion;
        ipEntryToUpdate.ZipCode = ipInfo.ZipCode;
        ipEntryToUpdate.VisitCount = ipEntryToUpdate.VisitCount + 1;
        _db.SaveChanges();
    }
}

