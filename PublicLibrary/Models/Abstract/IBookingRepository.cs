using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicLibrary.Models.Abstract
{
    public interface IBookingRepository
    {

        IEnumerable<Booking> GetAll();
        Booking Find(int? id);
        void InsertOrUpdate(Booking booking);
        void Delete(Booking booking);
    }
}
