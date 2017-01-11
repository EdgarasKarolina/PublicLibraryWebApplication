using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibrary.Models.Abstract
{
   public interface IReaderRepository
    {
        IEnumerable<Reader> GetAll();
        Reader Find(int? id);
        
    }
}
