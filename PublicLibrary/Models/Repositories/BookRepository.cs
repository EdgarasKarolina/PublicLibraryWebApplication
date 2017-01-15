using PublicLibrary.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicLibrary.Models.Repositories
{
    public class BookRepository : IBookRepository
    {

        private readonly ApplicationDbContext _db = new ApplicationDbContext();


        public void Delete(Book book)
        {
            _db.Books.Remove(book);
            _db.SaveChanges();
        }

        public Book Find(int? id)
        {
            return _db.Books.Find(id);
        }



        public IEnumerable<Book> GetAll()
        {
            return _db.Books;
        }

        public IEnumerable<Book> GetBooksWithTitle(string title)
        {
            return _db.Books.Where((s => s.Title == title));
        }

        public IEnumerable<Book> GetBooksWithGenre(string bookGenre)
        {
            return _db.Books.Where(x => x.Genre == bookGenre);
        }

        public IEnumerable<Book> GetBooksWithTitleAndGenre(string title, string bookGenre)
        {
            return _db.Books.Where((s => s.Title == title)).Where(x => x.Genre == bookGenre);
        }
        public void InsertOrUpdate(Book book)
        {
            if (book.BookId <= 0)
            {
                _db.Books.Add(book);
            }
            else
            {
                _db.Entry(book).State = System.Data.Entity.EntityState.Modified;
            }
            _db.SaveChanges();
        }
    }
}