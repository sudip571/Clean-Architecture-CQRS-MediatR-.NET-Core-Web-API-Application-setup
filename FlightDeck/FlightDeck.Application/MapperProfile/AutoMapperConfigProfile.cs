using AutoMapper;
using FlightDeck.Application.Features.References.Commands.CreateReference;
using FlightDeck.Application.Features.References.Commands.UpdateReference;
using FlightDeck.Application.Features.References.Queries.GetReferenceDetail;
using FlightDeck.Application.Features.References.Queries.GetReferenceList;
using FlightDeck.Application.LogUserLogins;
using FlightDeck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightDeck.Application.MapperProfile
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile()
        {
            CreateMap<ReferenceListVM, Reference>().ReverseMap();
            CreateMap<ReferenceDetailVM, Reference>().ReverseMap();
            CreateMap<CreateReferenceCommand, Reference>().ReverseMap();
            CreateMap<UpdateReferenceCommand, Reference>().ReverseMap();
            CreateMap<LogUserLoginVM, LogUserLogin>().ReverseMap();

        }
    }
}
