using Microsoft.AspNetCore.Mvc;
using BigBrotherRedux.Entities;
using BigBrotherRedux.Services.Interfaces;

namespace BigBrotherRedux.Controllers
{
    [Route("BigBrotherRedux/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {

        private readonly ISessionRepo _sessionRepo;

        public SessionController(ISessionRepo sessionRepo)
        {
            _sessionRepo = sessionRepo;
        }

        /// <summary>
        /// Gets a session by a particular session ID
        /// </summary>
        /// <param name="sessionID">A session ID which identifies a session for a particular user</param>
        /// <returns>A</returns>
        [HttpGet]
        [Route("GetSession/{sessionID:int}")]
        public Session Get(int sessionID)
        {
            var data = _sessionRepo.GetEntry(sessionID);
            return data;
        }

        /// Reads all entries in the Session Table in a list format.
        /// <returns></returns>
        [HttpGet]
        [Route("ReadAll")]
        public List<Session> ReadAllSessions()
        {


            var u = _sessionRepo.ReadAll();

            return u.ToList();


        }


        /// <summary>
        /// Posts a new entry into the database 
        /// This works now.
        /// </summary>
        /// <param name="data"></param>
        [HttpGet]
        [Route("CreateSession/{ip}/{date}/{login}/{purchase}")]
        public void Post(string ip, string date, string login, string purchase)
        {
            Session s = new Session();
            s.DateTime = DateTime.Parse(date.Replace("%2F", "/"));
            s.LoggedIn = login;
            s.PurchaseMade = purchase;
            s.UserIPAddress = ip;

            if (ModelState.IsValid)
            {
                _sessionRepo.CreateEntry(s);
            }

        }



        /// <summary>
        /// Puts a new entry into the session table
        /// </summary>
        /// <param name="dateAdded">When the session was added</param>
        /// <param name="logIn">Is the user logged in</param>
        /// <param name="purchased">Has the user purchased anything in this session</param>

        [HttpGet]
        [Route("EditSession/{id:int}/{ip}/{date}/{login}/{purchase}")]
        public void PutData(int id, string ip, string date, string login, string purchase)
        {
            Session s = new Session();
            s.DateTime = DateTime.Parse(date.Replace("%2F", "/"));
            s.LoggedIn = login;
            s.PurchaseMade = purchase;
            s.UserIPAddress = ip;
            s.SessionId = id;

            _sessionRepo.UpdateEntry(s);
        }

        /// <summary>
        /// Deletes an entry by id in the table 
        /// </summary>
        /// <param name="id">the id we want to delete</param>
        [HttpGet]
        [Route("DeleteSession/{id:int}")]
        public void Delete(int id)
        {
            Session s = _sessionRepo.GetEntry(id);
            s.SessionId = id;
            _sessionRepo.DeleteSessionRef(s);
        }


    }
}
