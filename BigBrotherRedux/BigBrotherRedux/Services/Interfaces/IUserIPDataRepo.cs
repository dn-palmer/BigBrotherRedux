using BigBrotherRedux.Entities;

namespace BigBrotherRedux.Services.Interfaces;
/// <summary>
/// Interface that contains various abstract methods that define
/// various actions to perform on the UserIPData table within the
/// database.
/// </summary>
public interface IUserIPDataRepo
{
    public ICollection<UserIPData> ReadAll();

    public void CreateEntry(UserIPData ipInfo);

    public void UpdateEntry(UserIPData ipInfo);

    public UserIPData GetEntry(string ip);

    public void DeleteEntry(string ip);

    public bool EntryExists(string ip);

}

