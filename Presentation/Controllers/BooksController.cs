using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class BooksController : ControllerBase
    {

        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookAsync([FromRoute(Name = "id")] int id)
        {

            var book = await _manager.BookService.GetOneBookByIdAsync(id, false);

            return Ok(book);

        }

        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        {

            if (bookDto == null)
            {
                return BadRequest();
            }

           var book =  await _manager.BookService.CreateOneBookAsync(bookDto);

            return StatusCode(201, book);


        }

        [HttpPut]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] BookDtoForUpdate bookDto)
        {


            if (bookDto is null)
                return BadRequest();

            if(!ModelState.IsValid)
                return UnprocessableEntity(ModelState); 

            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);

            return NoContent();


        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync(int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);
            return Ok();

        }

        [HttpPatch]
        public async Task<IActionResult> PartiallyUpdateOneBookAsync([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<BookDto> bookPatch)
        {

            var bookDto =  await _manager.BookService.GetOneBookByIdAsync(id, true);

            

            bookPatch.ApplyTo(bookDto);

            await _manager.BookService.UpdateOneBookAsync(id, new BookDtoForUpdate { 
            Id=bookDto.Id,
            Price=bookDto.Price,
            Title=bookDto.Title}, 
            true);

            return NoContent();

        }

    }
}

