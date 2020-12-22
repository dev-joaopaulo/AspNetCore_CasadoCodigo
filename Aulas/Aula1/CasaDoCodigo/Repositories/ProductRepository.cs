using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProductRepository : BaseRepository<Produto>, IProductRepository
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.ToList();
        }

        public void SaveProducts(List<Book> books)
        {
            foreach (var book in books)
            {
                if (!dbSet.Where(p => p.Codigo == book.Codigo).Any())
                {
                    dbSet.Add(new Produto(book.Codigo, book.Nome, book.Preco));
                }
            }
            context.SaveChanges();
        }
    }

    public class Book
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
