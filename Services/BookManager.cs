﻿using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;

        public BookManager(IRepositoryManager manager, ILoggerService logger)
        {
            _manager = manager;
            _logger = logger;
        }

        public Book CreateOneBook(Book book)
        {
            
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id,bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                string message = $"The book with id:{id} could not found.";
                _logger.LogInfo(message);
                throw new Exception(message);
            }
                _manager.Book.DeleteOneBook(entity);
                _manager.Save();
        }

        public IEnumerable<Book> GetAllBooks(bool trackChanges)
        {
            var books = _manager.Book.GetAllBooks(trackChanges);
            return books;
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book = _manager.Book.GetOneBookById(id,trackChanges);
            return book;
        }

        public void UpdateOneBook(int id, Book book, bool trackChanges)
        {
            var entity = _manager.Book.GetOneBookById(id, trackChanges);
            if (entity is null)
            {
                string msg = $"Book with id:{id} could not found.";
                _logger.LogInfo(msg);   
                throw new Exception();
            }

            if(book is null)
            {
                throw new ArgumentException(nameof(book));  
            }

            entity.Title = book.Title;
            entity.Price = book.Price;  

            _manager.Book.Update(entity);
            _manager.Save();
        }
    }
}
