using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IOrderRepository orderRepository;

        public PedidoController(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Carrossel()
        {
            return View(productRepository.GetProdutos());
        }

        public IActionResult Resumo()
        {
            Pedido pedido = orderRepository.GetOrder();

            return View(pedido);
        }

        public IActionResult Carrinho(string codigo)
        {

            if (!string.IsNullOrEmpty(codigo))
            {
                orderRepository.AddItem(codigo);
            }

            Pedido order = orderRepository.GetOrder(); 
            return View(order.Itens);
        }


    }
}
