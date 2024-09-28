﻿using Entities.Models;
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
            public IActionResult GetAllBooks()
            {

                try
                {
                    var books = _manager.BookService.GetAllBooks(false);
                    return Ok(books);
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            [HttpGet("{id:int}")]
            public IActionResult GetBook([FromRoute(Name = "id")] int id)
            {

                try
                {
                    var book = _manager.BookService.GetOneBookById(id, false);

                    if (book == null)
                    {
                        return NotFound();
                    }

                    return Ok(book);
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            [HttpPost]
            public IActionResult CreateOneBook([FromBody] Book book)
            {
                try
                {
                    if (book == null)
                    {
                        return BadRequest();
                    }

                    _manager.BookService.CreateOneBook(book);

                    return StatusCode(201, book);
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            [HttpPut]
            public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
            {
                try
                {

                    if (book is null)
                        return BadRequest();

                    _manager.BookService.UpdateOneBook(id, book, true);

                    return NoContent();

                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            [HttpDelete("{id:int}")]
            public IActionResult DeleteOneBook(int id)
            {

                try
                {
                    _manager.BookService.DeleteOneBook(id, false);
                    return Ok();
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            [HttpPatch]
            public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
            {
                try
                {

                    var entity = _manager.BookService.GetOneBookById(id, true);

                    if (entity is null)
                        return NotFound();

                    bookPatch.ApplyTo(entity);

                    _manager.BookService.UpdateOneBook(id, entity, true);

                    return NoContent();
                }

                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
    }
