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

        public PedidoController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
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
            return View();
        }

        public IActionResult Carrinho()
        {
            return View();
        }


    }
}
