using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Domain.Common;

namespace WebAPIRestaurant.Core.Domain.Entities
{
    public class Table : AuditableBaseEntity
    {
        public int CantPerson {  get; set; }
        public string? Description { get; set; }
        public int? OrdenId { get; set; }
        public Orden? Orden { get; set; }
        public int StatusId { get; set; }
        public Status? Status { get; set; }
    }
}
