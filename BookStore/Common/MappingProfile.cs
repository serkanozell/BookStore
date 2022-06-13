using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.Application.BookOperations.GetBooks;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.Entities;
using static BookStore.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();

            CreateMap<Book, BookDetailViewModel>().ForMember(
                dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.GenreName)).ForMember(
                dest => dest.AuthorFirstName,
                opt => opt.MapFrom(src => src.Author.FirstName)).ForMember(
                dest => dest.AuthorLastName,
                opt => opt.MapFrom(src => src.Author.LastName));

            CreateMap<Book, BooksViewModel>().ForMember(
                dest => dest.Genre,
                opt => opt.MapFrom(src => src.Genre.GenreName)).ForMember(
                dest => dest.AuthorFirstName,
                opt => opt.MapFrom(src => src.Author.FirstName)).ForMember(
                dest => dest.AuthorLastName,
                opt => opt.MapFrom(src => src.Author.LastName));

            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<CreateAuthorModel, Author>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();

            CreateMap<CreateUserModel, User>();
        }
    }
}
