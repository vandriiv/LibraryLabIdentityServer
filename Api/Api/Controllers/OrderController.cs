using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Exceptions;
using Domain.Services.Interfaces;
using LibraryLab.Extensions;
using LibraryLab.TokenUtil;
using LibraryLab.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryLab.Controllers
{
    [Route("api/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public OrderController(IOrderService orderService,
            IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        [Authorize]
        [HttpPost("orders")]
        public async Task<IActionResult> MakeOrder([FromBody]IEnumerable<OrderedBookViewModel> orderedBooks)
        {
            var jwtProvider = new JwtProvider();

            var data = jwtProvider.DecodeJwt(Request.Headers["Authorization"]);

            if (data != null)
            {
                var user = await _userService.GetUserByEmailAsync(data.Email);
                if (user != null)
                {
                    var orderData = orderedBooks.ConvertToUserBooks(user.Id);
                    try
                    {
                        await _orderService.MakeOrder(orderData);
                        return new ObjectResult("Success");
                    }
                    catch(OrderedBookAvailabilityException e)
                    {
                        return StatusCode(422, "Books available count has been updated. Please, check book availability");
                    }
                    catch(Exception e)
                    {
                        return StatusCode(500, "Server error. Try to reload page.");
                    }
                }
                else
                {
                    return StatusCode(401, "User is unauthorized");
                }
            }
            else
            {
                return StatusCode(401, "User is unauthorized");
            }           
        }
        
        [HttpPut("orders")]
        [Authorize(Policy ="admin")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateUserBookViewModel updatedUserBookData)
        {
            if (updatedUserBookData.OldCount <= updatedUserBookData.NewCount)
            {
                return StatusCode(400, "Invalid data format.");
            }

            try
            {
                await _orderService.UpdateBookCount(updatedUserBookData.UserId, updatedUserBookData.BookId
                    , updatedUserBookData.NewCount, updatedUserBookData.OldCount);
                return Ok("Updated!");
            }
            catch(Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }

        [HttpGet("orders/{email}")]
        [Authorize]
        public async Task<IActionResult> GetUserBooks(string email)
        {
            try
            {
                var userBooks = await _orderService.GetUserOrderedBooks(email);
                if (userBooks.Count() == 0)
                {
                    return new NotFoundObjectResult("You have not had ordered books yet.");
                }
                var userBooksViewModel = new UserBooksViewModel();
                userBooksViewModel.Books = userBooks.Select(b => new BookCount { Book = b.Book, Count = b.Count }).ToList();
                userBooksViewModel.User = userBooks.First().User;                

                return new ObjectResult(userBooksViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }

    }
}