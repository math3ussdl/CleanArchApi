namespace CleanArchApi.Application.Profiles;

using AutoMapper;
using Application.DTOs;
using Domain;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<Author, AuthorDto>().ReverseMap();
		CreateMap<Book, BookDto>().ReverseMap();
		CreateMap<Publisher, PublisherDto>().ReverseMap();
	}
}
