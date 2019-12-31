using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using PhoneBook.core.Models;
using PhoneBook.services;

namespace PhoneBook.Controllers
{
    public class TestingApiController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> TestApiCall()
        {
            return new string[] { "aa", "bb" };
        }
        //[HttpPost]
        //[Route("testing-api/clear")]
        //public async Task<bool> Clear()
        //{
        //    await ContactsService.DeleteContacts();
        //    return true;
        //}

        [HttpPost]
        [Route("testing-api/phone")]
        public JsonResult<PhoneNumber> GetNumber()
        {
            var model = new PhoneNumber();
            //var address = new Address();
            return Json(model);

        }
        [HttpPost]
        [Route("testing-api/address")]
        public JsonResult<Address> Get()
        {
            var model = new Address();
            //var address = new Address();
            return Json(model);

        }
        [HttpPost]
        [Route("testing-api/")]
        public JsonResult<Contact> GetContact()
        {
            var model = new Contact();
            //var address = new Address();
            return Json(model);

        }
        [HttpPost]
        [Route("testing-api/email")]
        public JsonResult<Email> GetEmail()
        {
            var model = new Email();
            //var address = new Address();
            return Json(model);

        }

       

    }
}
