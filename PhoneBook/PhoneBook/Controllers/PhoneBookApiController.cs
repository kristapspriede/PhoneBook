using AutoMapper;
using PhoneBook.core.Models;
using PhoneBook.services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace PhoneBook.Controllers
{
    public class PhoneBookApiController : ApiController
    {
        private readonly IContactsService _contactsService;
        private readonly IMapper _mapper;

        public PhoneBookApiController(IContactsService contactsService, IMapper mapper)
        {
            _contactsService = contactsService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("contact-api/contacts")]
        public async Task<HttpResponseMessage> AddContact(HttpRequestMessage request, ContactViewModel contact)
        {
            if (!IsValid(_mapper.Map<Contact>(contact)))
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, contact);
            }
            else
            {
                var contactDomainModel = _mapper.Map<Contact>(contact);
                await _contactsService.AddContact(contactDomainModel);
                var contactViewModel = _mapper.Map<ContactViewModel>(contactDomainModel);
                return request.CreateResponse(HttpStatusCode.Created, contactViewModel);
            }
        }

        [HttpPost]
        [Route("contact-api/address/{id}")]
        public async Task<HttpResponseMessage> AddAddress(HttpRequestMessage request, int id, AddressViewModel address)
        {
            var addressDomainModel = _mapper.Map<Address>(address);
            await _contactsService.AddAddress(id, addressDomainModel);
            var addressViewModel = _mapper.Map<AddressViewModel>(address);
            return request.CreateResponse(HttpStatusCode.Created, addressViewModel);
        }

        [HttpPost]
        [Route("contact-api/phone/{id}")]
        public async Task<HttpResponseMessage> AddPhoneNumber(HttpRequestMessage request, int id, PhoneNumberViewModel number)
        {
            var numberDomainModel = _mapper.Map<PhoneNumber>(number);
            await _contactsService.AddPhoneNumber(id, numberDomainModel);
            var numberViewModel = _mapper.Map<PhoneNumberViewModel>(number);
            return request.CreateResponse(HttpStatusCode.Created, numberViewModel);
        }

        [HttpPost]
        [Route("contact-api/email/{id}")]
        public async Task<HttpResponseMessage> AddEmail(HttpRequestMessage request, int id, EmailViewModel email)
        {
            var emailDomainModel = _mapper.Map<Email>(email);
            await _contactsService.AddEmail(id, emailDomainModel);
            var numberViewModel = _mapper.Map<EmailViewModel>(email);
            return request.CreateResponse(HttpStatusCode.Created, numberViewModel);
        }

        [HttpGet]
        [Route("contact-api/contacts")]
        public async Task<HttpResponseMessage> GetContacts(HttpRequestMessage request)
        {
            var contacts = await _contactsService.GetContacts();
            var contactViewModel = _mapper.Map<ICollection<Contact>>(contacts);
            return request.CreateResponse(HttpStatusCode.OK, contactViewModel);
        }

        [HttpGet]
        [Route("contact-api/address")]
        public async Task<HttpResponseMessage> GetAddresses(HttpRequestMessage request)
        {
            var addresses = await _contactsService.GetAddresses();
            var contactViewModel = _mapper.Map<ICollection<AddressViewModel>>(addresses);
            return request.CreateResponse(HttpStatusCode.OK, contactViewModel);
        }

        [HttpGet]
        [Route("contact-api/address/{id}")]
        public async Task<HttpResponseMessage> GetAddress(HttpRequestMessage request, int id)
        {
            var address = await _contactsService.GetAddress(id);
            var addressViewModel = _mapper.Map<List<AddressViewModel>>(address);
            return request.CreateResponse(HttpStatusCode.OK, addressViewModel);
        }

        [HttpGet]
        [Route("contact-api/phone/{id}")]
        public async Task<HttpResponseMessage> GetPhoneNumber(HttpRequestMessage request, int id)
        {
            var number = await _contactsService.GetPhoneNumber(id);
            var numberViewModel = _mapper.Map<List<PhoneNumberViewModel>>(number);
            return request.CreateResponse(HttpStatusCode.OK, numberViewModel);
        }

        [HttpGet]
        [Route("contact-api/email/{id}")]
        public async Task<HttpResponseMessage> GetEmail(HttpRequestMessage request, int id)
        {
            var email = await _contactsService.GetEmail(id);
            var emailViewModel = _mapper.Map<List<EmailViewModel>>(email);
            return request.CreateResponse(HttpStatusCode.OK, emailViewModel);
        }

        [HttpGet]
        [Route("contact-api/contacts/{id}")]
        public async Task<HttpResponseMessage> GetContactById(HttpRequestMessage request, int id)
        {
            var contact = await _contactsService.GetContactById(id);
            if (contact == null)
            {
                return request.CreateResponse(HttpStatusCode.NotFound);
            }
            return request.CreateResponse(HttpStatusCode.OK, contact);
        }

        [HttpGet]
        [Route("contact-api/types")]
        public async Task<HttpResponseMessage> GetContactTypes(HttpRequestMessage request)
        {
            var contacts = await _contactsService.GetAllTypes();
            return request.CreateResponse(HttpStatusCode.OK, contacts);
        }

        [HttpGet]
        [Route("contact-api/contacts/search/{search}")]
        public async Task<HttpResponseMessage> ContactSearch(HttpRequestMessage request, string search)
        {
            var contacts = await _contactsService.GetContacts(search);
            var result = new List<ContactViewModel>();
            foreach (var contact in contacts)
            {
                result.Add(_mapper.Map<ContactViewModel>(contact));
            }
            return request.CreateResponse(HttpStatusCode.OK, result);

            // contact-api/contacts?search=asdasdas
        }

        [HttpPut]
        [Route("contact-api/contacts/{id}")]
        public async Task<HttpResponseMessage> UpdateContact(HttpRequestMessage request, int id, ContactViewModel contact)
        {
            var contactDomainModel = _mapper.Map<Contact>(contact);
            await _contactsService.UpdateContact(id, contactDomainModel);
            var contactViewModel = _mapper.Map<ContactViewModel>(contactDomainModel);
            return request.CreateResponse(HttpStatusCode.OK, contactViewModel);
        }

        [HttpPut]
        [Route("contact-api/address/{contactId}/{id}")]
        public async Task<HttpResponseMessage> UpdateAddress(HttpRequestMessage request, int contactId, int id, AddressViewModel address)
        {
            var addressDomainModel = _mapper.Map<Address>(address);
            await _contactsService.UpdateAddress(contactId, id, addressDomainModel);
            var addressViewModel = _mapper.Map<AddressViewModel>(addressDomainModel);
            return request.CreateResponse(HttpStatusCode.OK, addressViewModel);
        }

        [HttpPut]
        [Route("contact-api/email/{contactId}/{id}")]
        public async Task<HttpResponseMessage> UpdateEmail(HttpRequestMessage request, int contactId, int id, EmailViewModel email)
        {
            var emailDomainModel = _mapper.Map<Email>(email);
            await _contactsService.UpdateEmail(contactId, id, emailDomainModel);
            var emailViewModel = _mapper.Map<EmailViewModel>(emailDomainModel);
            return request.CreateResponse(HttpStatusCode.OK, emailViewModel);
        }

        [HttpPut]
        [Route("contact-api/phone/{contactId}/{id}")]
        public async Task<HttpResponseMessage> UpdatePhoneNumber(HttpRequestMessage request, int contactId, int id, PhoneNumberViewModel number)
        {
            var numberDomainModel = _mapper.Map<PhoneNumber>(number);
            await _contactsService.UpdatePhoneNumber(contactId, id, numberDomainModel);
            var numberViewModel = _mapper.Map<PhoneNumberViewModel>(numberDomainModel);
            return request.CreateResponse(HttpStatusCode.OK, numberViewModel);
        }

        [HttpDelete]
        [Route("contact-api/contacts/{id}")]
        public async Task<HttpResponseMessage> DeleteById(HttpRequestMessage request, int id)
        {
            await _contactsService.DeleteContactById(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("contact-api/address/{id}")]
        public async Task<HttpResponseMessage> DeleteAddress(HttpRequestMessage request, int id)
        {
            await _contactsService.DeleteAddressById(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("contact-api/email/{id}")]
        public async Task<HttpResponseMessage> DeleteEmail(HttpRequestMessage request, int id)
        {
            await _contactsService.DeleteEmailById(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        [Route("contact-api/phone/{id}")]
        public async Task<HttpResponseMessage> DeletePhoneNumber(HttpRequestMessage request, int id)
        {
            await _contactsService.DeletePhoneNumberById(id);
            return request.CreateResponse(HttpStatusCode.OK);
        }

        // GET: api/PhoneBookApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        private bool IsValidBirthDay(string dateOfBirth)
        {
            if (!string.IsNullOrEmpty(dateOfBirth))
            {
                var birthDay = DateTime.Parse(dateOfBirth);
                var todayDate = DateTime.Today;
                var compare = DateTime.Compare(todayDate, birthDay);

                return compare > 0;
            }
            return false;
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                             + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                             + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }

        public bool IsValid(Contact contact)
        {
            return IsValidBirthDay(contact.DateOfBirth);
        }
    }
}