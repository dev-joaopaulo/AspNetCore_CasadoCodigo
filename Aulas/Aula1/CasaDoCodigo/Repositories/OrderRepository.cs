using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IOrderRepository
    {
        Pedido GetOrder();
        void AddItem(string code);
    }
    public class OrderRepository : BaseRepository<Pedido>, IOrderRepository
    {
        private readonly IHttpContextAccessor contextAccessor;

        public OrderRepository(ApplicationContext context, IHttpContextAccessor contextAccessor) : base(context)
        {
            this.contextAccessor = contextAccessor;
        }

        private int? GetOrderId()
        {
            return contextAccessor.HttpContext.Session.GetInt32("orderId");
        }

        private void SetOrderId(int orderId)
        {
            contextAccessor.HttpContext.Session.SetInt32("orderId", orderId);
        }

        public Pedido GetOrder()
        {
            var orderId = GetOrderId();
            var order = dbSet
                        .Include(p => p.Itens)
                            .ThenInclude(i => i.Produto)
                        .Where(p => p.Id == orderId)
                        .SingleOrDefault();

            if (order == null)
            {
                order = new Pedido();
                dbSet.Add(order);
                context.SaveChanges();
                SetOrderId(order.Id);
            }

            return order;

        }

        public void AddItem(string code)
        {
            var product = context.Set<Produto>()
                                    .Where(p => p.Codigo == code)
                                    .FirstOrDefault();

            if (product == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var order = GetOrder();

            var itemOrder = context.Set<ItemPedido>()
                                    .Where(i => i.Produto.Codigo == code &&
                                            i.Pedido.Id == order.Id)
                                    .SingleOrDefault();
                                    
            if (itemOrder == null)
            {
                itemOrder = new ItemPedido(order, product, 1, product.Preco);
                context.Set<ItemPedido>()
                    .Add(itemOrder);

                context.SaveChanges();
            }


        }
    }
}
