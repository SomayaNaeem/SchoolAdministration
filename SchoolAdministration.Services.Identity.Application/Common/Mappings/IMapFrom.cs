using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolAdministration.Services.Identity.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(AutoMapper.Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
