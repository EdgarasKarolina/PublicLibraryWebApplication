﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PublicLibrary.Models;

namespace PublicLibrary.Models.Abstract
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetBooksWithTitle(string title);
        IEnumerable<Book> GetBooksWithGenre(string bookGenre);
        IEnumerable<Book> GetBooksWithTitleAndGenre(string title, string bookGenre);
        Book Find(int? id);
        void InsertOrUpdate(Book book);
        void Delete(Book book);

    }
}