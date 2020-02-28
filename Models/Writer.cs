using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndyBooks.Models
{
    public class Writer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
