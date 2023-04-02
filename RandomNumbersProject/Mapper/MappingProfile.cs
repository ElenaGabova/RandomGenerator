using AutoMapper;
using Domain;
using Entities;

namespace Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ///Map from numbers to numbers entity
            CreateMap<Number, NumberEntity>().ForMember(x => x.Id, k => k.Ignore()).ReverseMap();
            CreateMap<Number, NumberEntity>().ForMember(x => x.Number, k => k.MapFrom(num => num.NumberValue)).ReverseMap();
           // CreateMap<Number, int>().ForMember(x => x., k => k.MapFrom(num => num.NumberValue));

            ///Map from NumberRepetition to NumberRepetitionEntity
            CreateMap<NumberRepetition, NumberRepetitionEntity>().ForMember(x => x.Id, k => k.Ignore()).ReverseMap();
            CreateMap<NumberRepetition, NumberRepetitionEntity>().ForMember(x => x.Number, k => k.MapFrom(num => num.NumberValue)).ReverseMap();
        }
    }
}