using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Application.ViewModels.Orden;
using WebAPIRestaurant.Core.Application.ViewModels.Table;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Core.Application.Interfaces.Services
{
    public interface ITableService : IGenericService<SaveTableViewModel, TableViewModel, Table>
    {
        Task UpdateTable(EditSaveViewModel ev, int id);
        Task<OrdenViewModel> GetTableOrden(int id);
        Task ChangeStatus(int tableId, int statusId);
    }
}
