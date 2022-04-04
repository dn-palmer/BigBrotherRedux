#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BBDisplay.Models;
using BBDisplay.Classes;
using Microsoft.AspNetCore.Authorization;

namespace BBDisplay.Controllers
{
    public class SessionsController : Controller
    {
        //Creating the client so that I can make calls to the API.
        private HttpClient client = new HttpClient();
        //Cleaner class. So that I can get ride of the Json formmating. JSon Serialzation would have worked bettter! 
        //Note to future me.
        private SessionsClean cleaner = new SessionsClean();
        private string apiIPaddress = "34.125.193.123";



        // GET: Sessions


        public async Task<IActionResult> Index()
        {
            SessionsClean cleaner = new SessionsClean();

            var data = await client.GetStringAsync($"http://{apiIPaddress}/BigBrotherRedux/Session/ReadAll");
                data = cleaner.RemoveSquareBraces(data);
                List<string> sessIn = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
                var model = cleaner.IndexPrepSessions(sessIn);
                return View(model);

   
          

        }

        // GET: Sessions/Details/5

        public async Task<IActionResult> Details(string id)
        {
            var data = await client.GetStringAsync($"http://{apiIPaddress}/BigBrotherRedux/Session/GetSession/{id}");
            data = cleaner.RemoveSquareBraces(data);
            List<string> sessIn = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrepSessions(sessIn);
            return View(model[0]);
        }

        // GET: Sessions/Create
        [Authorize]

        public IActionResult Create()
        {
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create(Session session)
        {
            string ip, date, login, purchase;

            ip = session.UserIPAddress;
            date = session.DateTime;
            login = session.LoggedIn;
            purchase = session.PurchaseMade;

            var data = await client.GetStringAsync($"http://{apiIPaddress}/BigBrotherRedux/Session/CreateSession/{ip}/{date}/{login}/{purchase}");
            return RedirectToAction("Index");
        }

        // GET: Sessions/Edit/5
        [Authorize]

        public async Task<IActionResult> Edit(int? id)
        {
            var data = await client.GetStringAsync($"http://{apiIPaddress}/BigBrotherRedux/Session/GetSession/{id}");
            data = cleaner.RemoveSquareBraces(data);
            List<string> sessIn = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrepSessions(sessIn);
            return View(model[0]);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> EditPost(int id, [Bind("SessionId,UserIPAddress,DateTime,LoggedIn,PurchaseMade")] Session session)
        {
            string ip, date, login, purchase;
            ip = session.UserIPAddress;
            date = session.DateTime;
            login = session.LoggedIn;
            purchase = session.PurchaseMade;

            var data = await client.GetStringAsync($"http://{apiIPaddress}/BigBrotherRedux/Session/EditSession/{id}/{ip}/{date}/{login}/{purchase}");
            return RedirectToAction("Index");      
        }

        // GET: Sessions/Delete/5
        [Authorize]

        public async Task<IActionResult> Delete(int? id)
        {
            var data = await client.GetStringAsync($"http://{apiIPaddress}BigBrotherRedux/Session/GetSession/{id}");
            data = cleaner.RemoveSquareBraces(data);
            List<string> sessIn = cleaner.PreppedData(cleaner.CleanAPIResponse(data));
            var model = cleaner.IndexPrepSessions(sessIn);
            return View(model[0]);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await client.GetStringAsync($"http://{apiIPaddress}/BigBrotherRedux/Session/DeleteSession/{id}");
            return RedirectToAction("Index");
        }

    }
}
