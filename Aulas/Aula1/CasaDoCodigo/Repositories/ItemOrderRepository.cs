using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IItemOrderRepository
    {

    }
    public class ItemOrderRepository : BaseRepository<ItemPedido>, IItemOrderRepository
    {
        public ItemOrderRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
