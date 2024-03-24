using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.ViewModels.DisheOrden;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Interfaces.Services
{
    public interface IDishesOrdenService : IGenericService<SaveDishesOrdenViewModel, DishesOrdenViewModel, DishesOrden>
    {
    }
}
