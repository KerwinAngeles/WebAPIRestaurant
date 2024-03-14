using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.ViewModels.Dishe;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Interfaces.Services
{
    public interface IDisheService : IGenericService<SaveDisheViewModel, DisheViewModel, Dishe>
    {
        Task<List<DisheViewModel>> GetAllWithInclude();
    }
}
