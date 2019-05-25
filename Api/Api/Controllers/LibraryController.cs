using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using Domain.Services.Interfaces;
using LibraryLab.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryLab.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly IBookService _bookService;
        public LibraryController(IBookService bookService)
        {
            _bookService = bookService;
        }        

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks(int limit,int offset)
        {
            if (limit < 1 || offset < 0)
            {
                return StatusCode(400, "Invalid data format.");
            }

            try
            {
                var books = await _bookService.GetBooksRangeAsync(limit + 1, offset);

                var booksListViewModel = new BooksListViewModel();
                booksListViewModel.Books = books.Take(limit);

                if (books.Count() > limit)
                {
                    booksListViewModel.HasMore = true;
                }
                else
                {
                    booksListViewModel.HasMore = false;
                }

                return new ObjectResult(booksListViewModel);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }

        [HttpGet("books/{genreName}/genres")]
        public async Task<IActionResult> GetBooksByGenreName(string genreName,int limit, int offset)
        {
            if (genreName == "" || limit < 1 || offset < 0)
            {
                return StatusCode(400, "Invalid data format");
            }

            try
            {           
                var books = await _bookService.GetBooksRangeByGenreNameAsync(genreName,limit + 1, offset);

                var booksListViewModel = new BooksListViewModel();

                if (books.Count() == 0)
                {
                    return new NotFoundObjectResult("There are not books by this genre");
                }
                booksListViewModel.Books = books.Take(limit);


                if (books.Count() > limit)
                {
                    booksListViewModel.HasMore = true;
                }
                else
                {
                    booksListViewModel.HasMore = false;
                }

                return new ObjectResult(booksListViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }

        [HttpGet("books/{authorId}/authors")]
        public async Task<IActionResult> GetBooksByAuthorId(long authorId,int limit, int offset)
        {
            if (limit < 1 || offset < 0)
            {
                return StatusCode(400, "Invalid data format.");
            }

            try
            {
                var books = await _bookService.GetBooksRangeByAuthorIdAsync(authorId,limit + 1, offset);

                var booksListViewModel = new BooksListViewModel();

                if (books.Count() == 0)
                {
                    return new NotFoundObjectResult("There are not books by this author.");
                }

                booksListViewModel.Books = books.Take(limit);

                if (books.Count() > limit)
                {
                    booksListViewModel.HasMore = true;
                }
                else
                {
                    booksListViewModel.HasMore = false;
                }

                return new ObjectResult(booksListViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }

        [HttpGet("books/{title}")]
        public async Task<IActionResult> GetBooksByTitle(string title, int limit, int offset)
        {
            if (title==""||limit < 1 || offset < 0)
            {
                return StatusCode(400, "Invalid data format.");
            }

            try
            {
                var books = await _bookService.GetBooksRangeByTitleAsync(title, limit + 1, offset);

                var booksListViewModel = new BooksListViewModel();

                if (books.Count() == 0)
                {
                    return new NotFoundObjectResult("There are not books by this title.");
                }
                booksListViewModel.Books = books.Take(limit);


                if (books.Count() > limit)
                {
                    booksListViewModel.HasMore = true;
                }
                else
                {
                    booksListViewModel.HasMore = false;
                }

                return new ObjectResult(booksListViewModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }
        

        [HttpPost("books/availableCount")]
        public async Task<IActionResult> GetBooksAvailableCountAsync(IEnumerable<long> books)
        {
            try
            {
                var booksAvailablity = await _bookService.GetBooksAvailableCountAsync(books);
                return new ObjectResult(booksAvailablity);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }

        [HttpPost("books")]
        [Authorize(Policy="admin")]
        public async Task<IActionResult> AddBook([FromBody] NewBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newBook = Mapper.Map<Book>(book);
                    await _bookService.AddBookAsync(newBook);
                    return Ok("Book successfully added");
                }
                catch (MemberRelationException e)
                {
                    return StatusCode(400, e.Message + " "+ e.Column );
                }
                catch(Exception e)
                {
                    return StatusCode(500, "Server error. Try to reload page.");
                }
            }
            else
            {
                return StatusCode(400, "Invalid data format.");
            }
        }

        [HttpPut("books")]
        [Authorize(Policy="admin")]
        public async Task<IActionResult> UpdateBook([FromBody] NewBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newBook = Mapper.Map<Book>(book);
                    await _bookService.UpdateBookAsync(newBook);
                    return Ok("Book successfully updated");
                }
                catch (MemberRelationException e)
                {
                    return StatusCode(400, e.Message + " " + e.Column);
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Server error. Try to reload page.");
                }
            }
            else
            {
                return StatusCode(400, "Invalid data format.");
            }
        }

        [HttpPost("authors")]
        [Authorize(Policy="admin")]
        public async Task<IActionResult> AddAuthor([FromBody] NewAuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newAuthor = Mapper.Map<Author>(author);
                    await _bookService.AddAuthorAsync(newAuthor);
                    return Ok("Author successfully added");
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Server error. Try to reload page.");
                }
            }
            else
            {
                return StatusCode(400, "Invalid data format.");
            }
        }

        [HttpPut("authors")]
        [Authorize(Policy="admin")]
        public async Task<IActionResult> UpdateAuthor([FromBody] NewAuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newAuthor = Mapper.Map<Author>(author);
                    await _bookService.UpdateAuthorAsync(newAuthor);
                    return Ok("Author successfully updated");
                }
                catch (Exception e)
                {
                    return StatusCode(500, "Server error. Try to reload page.");
                }
            }
            else
            {
                return StatusCode(400, "Invalid data format.");
            }
        }

        
        [HttpGet("authors")]
        public async Task<IActionResult> GetAuthors()
        {
            try
            {
                var authors = await _bookService.GetAllAuthorsAsync();
                return new ObjectResult(authors);
            }
            catch(Exception e)
            {
                return StatusCode(500, "Server error. Try to reload page.");
            }
        }
    }
}