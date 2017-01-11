using PublicLibrary.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicLibrary.Models.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public Reader Find(int? id)
        {
            return db.Readers.Find(id);
        }


        public IEnumerable<Reader> GetAll()
        {
            return db.Readers;
        }


    }
}