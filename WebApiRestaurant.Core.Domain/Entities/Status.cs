using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIRestaurant.Core.Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Table> Tables { get; set; }
    }
}
