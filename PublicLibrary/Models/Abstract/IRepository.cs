using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibrary.Models.Abstract
{
    interface IRepository<TEntiry> where TEntiry : class
    {
        TEntiry Get(int id);
        IEnumerable<TEntiry> GetAll();
       

        void Add(TEntiry entity);
        void Remove(TEntiry entity);
    }
}
