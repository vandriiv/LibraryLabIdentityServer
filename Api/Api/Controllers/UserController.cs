using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.Services.Interfaces;
using LibraryLab.Extensions;
using LibraryLab.TokenUtil;
using LibraryLab.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace LibraryLab.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("session")]
        public async Task<IActionResult> Login([FromBody] UserLoginViewModel userLogin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.LoginAsync(userLogin.Email, userLogin.Password);

                    var identity = ConfigureIdentity.GetIdentity(user.Email, user.RoleId);
                    var jwtProvider = new JwtProvider();

                    var encodedJwt = jwtProvider.GetEncodedJwt(identity);

                    return new ObjectResult(new
                    {
                        access_token = encodedJwt,
                        userInfo = new TokenData
                        {
                            Email = user.Email,
                            RoleId = user.RoleId
                        }
                    });                    
                   
                }
                else
                {
                    return BadRequest(ModelState.Errors());
                }
            }
            catch(UserNotFoundException e)
            {
                return StatusCode(400, e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }

        [HttpPost("users")]
        public async Task<IActionResult> Registration([FromBody] UserRegistrationViewModel newUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = Mapper.Map<User>(newUser);
                    await _userService.RegistrationAsync(user);
                    return Ok("Welcome, " + user.Email + ", now you can login");
                }
                else
                {
                    return BadRequest(ModelState.Errors());
                }
                
            }
            catch(UserIsAlreadyExistException e)
            {
                return new JsonResult(e.Message);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }

        [Authorize]
        [HttpGet("userStatus")]
        public IActionResult GetUserStatus()
        {
            var jwtProvider = new JwtProvider();          

            var data = jwtProvider.DecodeJwt(Request.Headers["Authorization"]);

            return new ObjectResult(data);
        }
    }
}