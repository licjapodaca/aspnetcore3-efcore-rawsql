using AutoMapper;
using EFCore3.Dto;
using EFCore3.Entities;

namespace EFCore3.AutomapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
		{
			CreateMap<Author, AuthorDto>().ReverseMap();
			CreateMap<Book, BookDto>().ReverseMap();
				// .ForMember(dest => dest.Age, opt => opt.MapFrom(d => d.DateOfBirth.CalculateAge()))
				// .ForMember(dest => dest.CompleteName, opt =>
				// {
				// 	opt.MapFrom(d => string.Format("{0} {1}", d.FirstName, d.LastName));
				// });
		}
    }
}