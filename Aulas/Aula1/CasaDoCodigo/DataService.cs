using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    class DataService : IDataService
    {
        private readonly ApplicationContext context;
        private readonly IProductRepository productRepository;

        public DataService(ApplicationContext context,
            IProductRepository productRepository)
        {
            this.context = context;
            this.productRepository = productRepository;
        }

        public void StartDb()
        {
            context.Database.EnsureCreated();
            List<Book> books = GetBooks();

            productRepository.SaveProducts(books);
        }



        private static List<Book> GetBooks()
        {
            var json = File.ReadAllText("C:\\Users\\joaop\\source\\repos\\Alura\\_Recursos\\dados\\livros.json");
            var books = JsonConvert.DeserializeObject<List<Book>>(json);
            return books;
        }
    }




}
