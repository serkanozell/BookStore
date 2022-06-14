using AutoMapper;
using BookStore.Application.UserOperations.Commands.CreateToken;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.Application.UserOperations.Commands.DeleteUser;
using BookStore.Application.UserOperations.Commands.RefreshToken;
using BookStore.Application.UserOperations.Commands.UpdateUser;
using BookStore.Application.UserOperations.Queries.GetUserDetail;
using BookStore.Application.UserOperations.Queries.GetUsers;
using BookStore.DBOperations;
using BookStore.TokenOperations.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public UserController(IBookStoreDbContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            GetUsersQuery query = new GetUsersQuery(_context, _mapper);
            var result = query.Handle();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetUserDetailQuery getUserDetailQuery = new GetUserDetailQuery(_context, _mapper);
            getUserDetailQuery.UserId = id;

            GetUserDetailQueryValidator validation = new GetUserDetailQueryValidator();
            validation.ValidateAndThrow(getUserDetailQuery);

            var result = getUserDetailQuery.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand createUserCommand = new CreateUserCommand(_context, _mapper);
            createUserCommand.Model = newUser;

            CreateUserCommandValidator validation = new CreateUserCommandValidator();
            validation.ValidateAndThrow(createUserCommand);

            createUserCommand.Handle();

            return Ok("Kullanıcı basariyla Kaydedildi");
        }

        [HttpPut]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserModel updateUserModel)
        {
            UpdateUserCommand updateUserCommand = new UpdateUserCommand(_context);
            updateUserCommand.UserId = id;
            updateUserCommand.Model = updateUserModel;

            UpdateUserCommandValidator validation = new UpdateUserCommandValidator();
            validation.ValidateAndThrow(updateUserCommand);

            updateUserCommand.Handle();

            return Ok("Kullanıcı bilgileri başarıyla güncellenmiştir");
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand(_context);
            deleteUserCommand.UserId = id;

            DeleteUserCommandValidator validation = new DeleteUserCommandValidator();
            validation.ValidateAndThrow(deleteUserCommand);

            deleteUserCommand.Handle();

            return Ok("Kullanıcı başarıyla silinmiştir");
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
            command.Model = login;
            var token = command.Handle();
            return token;
        }

        [HttpGet("refreshToken")]
        public ActionResult<Token> RefreshToken([FromQuery] string token)
        {
            RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
            command.RefreshToken = token;
            var result = command.Handle();
            return result;
        }
    }
}
