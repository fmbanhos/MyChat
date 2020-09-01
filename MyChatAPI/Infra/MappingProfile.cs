using AutoMapper;
using MyChatAPI.Domain.Commands;
using MyChatAPI.Domain.Entities;

namespace MyChatAPI.Infra
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<GroupCreateCommandRequest, GroupEntity>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(orig => orig.Name));

			CreateMap<PersonCreateCommandRequest, PersonEntity>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(orig => orig.Name))
				.ForMember(dest => dest.IdGroup, opt => opt.MapFrom(orig => orig.IdGroup));
		}
	}
}
