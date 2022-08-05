namespace CleanArchApi.Application.Profiles;

using AutoMapper;
using DTOs.Author;
using DTOs.Book;
using DTOs.Publisher;
using Domain;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		#region Author Mappings
		CreateMap<Author, AuthorCreateDto>().ReverseMap();
		CreateMap<Author, AuthorDetailDto>().ReverseMap();
		CreateMap<Author, AuthorListDto>().ReverseMap();
		#endregion Author

		#region Book Mappings
		CreateMap<Book, BookCreateDto>().ReverseMap();
		CreateMap<Book, BookDetailDto>().ReverseMap();
		#endregion Book

		#region Publisher Mappings
		CreateMap<Publisher, PublisherCreateDto>().ReverseMap();
		CreateMap<Publisher, PublisherDetailDto>().ReverseMap();
		#endregion Publisher
	}
}
