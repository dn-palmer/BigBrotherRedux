using BigBrotherRedux.Entities;

namespace BigBrotherRedux.Services.Interfaces;
public interface ISessionRepo
{

    public ICollection<Session> ReadAll();
    public void CreateEntry(Session session);
    public Session GetEntry(int id);
    public void UpdateEntry(Session session);
    public void DeleteSessionRef(Session session);

}

