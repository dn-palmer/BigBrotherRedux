using BigBrotherRedux.Entities;
using BigBrotherRedux.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBrotherRedux.Controllers
{
    [Route("BigBrotherRedux/[controller]")]
    [ApiController]
    public class PageReferenceController : ControllerBase
    {
        private readonly IPageReferenceRepo _pageReferenceRepo;

        public PageReferenceController(IPageReferenceRepo pageReferenceRepo)
        {
            _pageReferenceRepo = pageReferenceRepo;
        }


        /// <summary>
        /// Method to handle a HTTP GET statement to get the ID of
        /// a specified page reference.
        /// </summary>
        /// <param name="interactionToGetID">ID of the page reference to get.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPageReference/{pageReference}")]
        public string GetPageReference(int pageReference)
        {
            PageReference data = _pageReferenceRepo.GetEntry(pageReference.ToString());
            if (data != null)
            {
                return (data.PageId + "\n" + data.DateAdded + "\n" + data.PageDescription);
            }
            else
                return null;

            // Get an existing page reference within the database
        }



        /// Reads all entries in the Page Reference Table in a list format.
        /// <returns></returns>
        [HttpGet]
        [Route("ReadAll")]
        public List<PageReference> ReadAllReferences()
        {
            var u = _pageReferenceRepo.ReadAll();
            return u.ToList();
        }

        /// <summary>
        /// Makes a post query to the database
        /// DP: This didnt do anything. Nothing called or anything. Gave it functionality. 
        /// </summary>
        /// <param name="data">The array of strings which we want to add to the database</param>
        [HttpPost]
        [Route("CreatePageRefrence/{dateAdded}/{pageDescription}")]
        public void Post(string dateAdded, string pageDescription)
        {
            PageReference p = new PageReference();
            p.PageDescription = pageDescription;
            dateAdded = dateAdded.Replace("%2F", "/");
            p.DateAdded = DateTime.Parse(dateAdded);

            if (ModelState.IsValid)
            {
                _pageReferenceRepo.CreateEntry(p);
            }
        }

        /// <summary>
        /// Puts an entry into the database with a certain date and description
        /// DP: Slight edit to this one. A PUT call updates an entry that is alread in the 
        /// datbase. I adjusted the method to reflect that. POST creates an all new entry
        /// which PUT can also do but we have both in this controller so we should use it   
        /// for editing. 
        /// </summary>
        /// <param name="dateAdded">The date when the page was added</param>
        /// <param name="description">The description of the page</param>
        [HttpPut]
        [Route("EditPageReference/{pageId:int}/{dateAdded}/{description}")]
        public void Put(int pageId,string dateAdded, string description)
        {
            PageReference p = new PageReference();
            p.PageId = pageId;
            dateAdded = dateAdded.Replace("%2F", "/");
            p.DateAdded = DateTime.Parse(dateAdded);
            p.PageDescription = description;
            _pageReferenceRepo.UpdateEntry(p);
        }

        /// <summary>
        /// Deletes an id in the page reference table
        /// </summary>
        /// <param name="id">A number that visually </param>
        [HttpDelete]
        [Route("DeletePageReference/{id:int}")]
        public void Delete(int id)
        {
            PageReference p = new PageReference();
            p.PageId = id;
            _pageReferenceRepo.DeletePageRef(p);
        }
    }
}
