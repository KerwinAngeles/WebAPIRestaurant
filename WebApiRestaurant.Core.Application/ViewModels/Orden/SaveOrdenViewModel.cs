using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.Enums;
using WebAPIRestaurant.Core.Application.ViewModels.Table;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.ViewModels.Orden
{
    public class SaveOrdenViewModel
    {
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "The subTotal is required")]
        public int SubTotal { get; set; }

        [Required(ErrorMessage = "The state is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "The subTotal is required")]
        public int TableId { get; set; }
        public List<int> DishesOrden { get; set; } = new List<int>();

    }
}
