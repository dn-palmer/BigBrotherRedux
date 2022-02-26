using BigBrotherRedux.Entities;

namespace BigBrotherRedux.Services.Interfaces;

public interface IUserIPDataRepo
{
    public ICollection<UserIPData> ReadAll();

    public void CreateEntry(UserIPData ipInfo);

    public void UpdateEntry(UserIPData ipInfo);

    public UserIPData GetEntry(string ip);

    public void DeleteEntry(string ip);

    public bool EntryExists(string ip);

}

