using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PhoneBook.core.Models;

namespace PhoneBook.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Contact, ContactViewModel>()
                    .ForMember(c => c.Addresses, co => co.MapFrom(x => x.Addresses))
                    .ForMember(c => c.Emails, co => co.MapFrom(x => x.Emails))
                    .ForMember(c => c.PhoneNumbers, co => co.MapFrom(x => x.PhoneNumbers));

                cfg.CreateMap<ContactViewModel, Contact>()
                    .ForMember(c => c.Addresses, co => co.MapFrom(x => x.Addresses))
                    .ForMember(c => c.Emails, co => co.MapFrom(x => x.Emails))
                    .ForMember(c => c.PhoneNumbers, co => co.MapFrom(x => x.PhoneNumbers));

                cfg.CreateMap<AddressViewModel, Address>()
                    .ForMember(d => d.Contact, s => s.Ignore()); 
                cfg.CreateMap<Address, AddressViewModel>();
                cfg.CreateMap<Email, EmailViewModel>();
                cfg.CreateMap<EmailViewModel, Email>()
                    .ForMember(d => d.Contact, s => s.Ignore());
                cfg.CreateMap<PhoneNumber, PhoneNumberViewModel>();
                cfg.CreateMap<PhoneNumberViewModel, PhoneNumber>()
                    .ForMember(d => d.Contact, s => s.Ignore());
                cfg.CreateMap<ContactType, ContactTypeViewModel>();
                cfg.CreateMap<ContactTypeViewModel, ContactType>();

            });

            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();
            return mapper;

        }
    }
}