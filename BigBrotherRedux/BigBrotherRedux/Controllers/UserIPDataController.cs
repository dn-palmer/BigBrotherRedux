using BigBrotherRedux.Entities;
using BigBrotherRedux.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBrotherRedux.Controllers
{
    [Route("BigBrotherRedux/[controller]")]
    [ApiController]
    public class UserIPDataController : ControllerBase
    {
        private readonly IUserIPDataRepo _userIPDataRepo;

        public UserIPDataController(IUserIPDataRepo userIPDataRepo)
        {
            _userIPDataRepo = userIPDataRepo;
        }


        /// Gets the current users IP Address and validates its existence. If it does not exist the 
        /// a call is made to Create a new entry. If it does exist a call is made to update an existing
        /// entry.
        /// <param name="ip"></param>
        [HttpGet]
        [Route("GetUserIP/{ip}")]
        public void GetIP(string ip)
        {

            if (_userIPDataRepo.EntryExists(ip))
            {
                Edit(ip);

            }
            else
            {
                Create(ip);

            }


        }


        /// Reads all entries in the UserIPData Table in a list format.
        /// <returns></returns>
        [HttpGet]
        [Route("ReadAll")]
        public List<UserIPData> ReadAllUsers()
        {


            var u = _userIPDataRepo.ReadAll();

            return u.ToList();


        }



        /// Reads a specifit entry from the USerIpData table and returns a UserIpDataObject
        /// <returns></returns>
        [HttpGet]
        [Route("ReadUser/{ip}")]
        public UserIPData ReadUsers(string ip)
        {

            var u = _userIPDataRepo.GetEntry(ip);
            return u;
        }



        [HttpPost]
        [Route("CreateUser/{ip}")]
        // POST: UserIPDataController/Create/ip
        //Creates a new entry in the UserIPData Table.
        public void Create(string ip)
        {
            GeoDataCleanUp cleaner = new GeoDataCleanUp();
            UserIPData newEntry = new UserIPData();
            string cleanData = cleaner.GetIPAPIResponse(ip);
            string[] prepedData = cleaner.DatabaseReadyData(cleanData);

            newEntry.CountryName = prepedData[0];
            newEntry.CountryCode = prepedData[1];
            newEntry.StateOrRegion = prepedData[2];
            newEntry.City = prepedData[3];
            newEntry.ZipCode = prepedData[4];
            newEntry.DeviceType = prepedData[5];
            newEntry.UserIP = prepedData[6];
            newEntry.VisitCount = 1;
            _userIPDataRepo.CreateEntry(newEntry);


        }

        [HttpPut]
        [Route("EditUser/{ip}")]
        // Put: UserIPDataController/Edit/ip
        //Updates a entry in the UserIPData Table.
        public void Edit(string ip)
        {
            GeoDataCleanUp cleaner = new GeoDataCleanUp();
            UserIPData updatedEntry = _userIPDataRepo.GetEntry(ip.Trim());
            string cleanData = cleaner.GetIPAPIResponse(ip);
            string[] prepedData = cleaner.DatabaseReadyData(cleanData);

            updatedEntry.CountryName = prepedData[0];
            updatedEntry.CountryCode = prepedData[1];
            updatedEntry.StateOrRegion = prepedData[2];
            updatedEntry.City = prepedData[3];
            updatedEntry.ZipCode = prepedData[4];
            updatedEntry.DeviceType = prepedData[5];
            updatedEntry.UserIP = prepedData[6];
            updatedEntry.VisitCount = updatedEntry.VisitCount + 1;
            _userIPDataRepo.UpdateEntry(updatedEntry);
        }

        [HttpDelete]
        [Route("DeleteUser/{ip}")]
        // GET: UserIPDataController/Delete/ip
        //Deletes a user ip from the UserIPData Table.
        public void Delete(string ip)
        {
            _userIPDataRepo.DeleteEntry(ip);
        }

    }
}
