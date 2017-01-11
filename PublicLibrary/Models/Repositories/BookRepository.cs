using PublicLibrary.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicLibrary.Models.Repositories
{
    public class BookRepository : IBookRepository
    {

        private ApplicationDbContext db = new ApplicationDbContext();

       
        public void Delete(Book book)
        {
             db.Books.Remove(book);
             db.SaveChanges();
        }

        public Book Find(int? id)
        {
            return db.Books.Find(id);
        }

   

public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }

        public void InsertOrUpdate(Book book)
        {
            if (book.BookId <= 0)
            {
                db.Books.Add(book);
            }
            else
            {
                db.Entry(book).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }
    }
}