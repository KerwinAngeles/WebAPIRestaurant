using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Enums;
using WebAPIRestaurant.Core.Application.ViewModels.Orden;

namespace WebAPIRestaurant.Core.Application.ViewModels.Table
{
    public class SaveTableViewModel
    {
        public virtual int Id { get; set; }
        public int CantPerson { get; set; }
        public string? Description { get; set; }
        private int statusId { get; set; }
        public int StatusId 
        {
            get
            {
                return statusId;
            }
            set
            {
                statusId = 1;
            }
        }
    }
}
