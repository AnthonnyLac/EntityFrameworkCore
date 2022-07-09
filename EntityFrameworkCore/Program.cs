using EntityFrameworkCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace EntityFrameworkCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.ApplicationContext();

            var exists = db.Database.GetAppliedMigrations().Any();

            if (exists)
            {
                //Regra de negocio
            }

            InsertData();
            //InsertMultipleData();
            //ConsultData();
            //RegisterOrder();
            //ConsultAdvancedLoadData();
            //UpdateData();
            //DeleteData();
        }
        private static void DeleteData()
        {
            using var db = new Data.ApplicationContext();

            //var client = db.Clients.Find(3);
            //db.Remove(client);
            //db.Clients.Remove(client);

            var client = new Client { Id = 4 };
            db.Entry(client).State = EntityState.Deleted;

            db.SaveChanges();

            Console.ReadLine();


        }

        private static void UpdateData() 
        {
            using var db = new Data.ApplicationContext();
            //var client = db.Clients.Find(1);

            //client.Name = "Pinky Kirby";

            //db.Clients.Update(client); Caso queria realizar um update em todas as colunas

            var client = new Client
            {
                Id = 1
            };

            var anonymousUser = new
            {
                Name = "anonymousUser 4",
                PhoneNumber = "40028922"
            };
            
            db.Attach(client); //Torna desnecessario fazer uma consulta ao banco
            db.Entry(client).CurrentValues.SetValues(anonymousUser);

            db.SaveChanges();
        }

        private static void ConsultAdvancedLoadData() 
        {
            using var db = new Data.ApplicationContext();
            //var orders = db.Orders.ToList(); //Não carrega informações das tabelas estrangeiras
            var orders = db.Orders
                .Include(p => p.items)
                .ThenInclude(p => p.Product)
                .ToList(); 

            Console.WriteLine(orders.Count);
        }
        private static void RegisterOrder()
        {
            using var db = new Data.ApplicationContext();
            var product = db.Products.FirstOrDefault();
            var client = db.Clients.FirstOrDefault();

            var order = new Order
            {
                ClientId = client.Id,
                Name = client.Name,
                StartedIn = DateTime.Now,
                EndIn = DateTime.Now,
                observation = "Teste",
                OrderStatus = ValueObjects.OrderStatus.Analise,
                TypeFreigth = ValueObjects.TypeFreight.Freeshipping,
                items = new List<OrderItem>()
                {
                    new OrderItem
                    {
                        ProductId = product.Id,
                        Discount = 0,
                        Amount = 1,
                        Value = 0
                    }
                }
            };

            db.Orders.Add(order);
            db.SaveChanges();
            Console.ReadLine();

        }
        private static void ConsultData() 
        {
            using var db = new Data.ApplicationContext();

            //var QueryBySyntax = (from c in db.Clients where c.Id > 0 select c).ToList();
            var QueryByMethod = db.Clients
                .Where(c => c.Id > 0)
                .OrderBy(c => c.Name)
                .ToList();
            
           

            foreach (var client in QueryByMethod) 
            {
                Console.WriteLine($"looking for client number: {client.Id}");
                //db.Clients.Find(client.Id); //Realiza a busca primeiramente em memoria, caso não haja ele realiza a consulta em nossa base de dados. 
                db.Clients.FirstOrDefault(p => p.Id == client.Id);
                Console.ReadLine();
            };

        }

        private static void InsertData()
        {
            var product = new Product
            {
                Description = "Test Product",
                BarCode = "4002892200000",
                Value = 10m,
                ProductType = ValueObjects.ProductType.Service,
                Active = true
            };

            using var db = new Data.ApplicationContext();

            db.Products.Add(product);
            //db.Set<Product>().Add(product);
            //db.Entry(product).State = EntityState.Added;
            //db.Add(product);

            var records = db.SaveChanges();
            Console.WriteLine($"Total saved record(s): {records}");
        }

        private static void InsertMultipleData()
        {
            var product = new Product
            {
                Description = "Golden ring",
                BarCode = "4002892200001",
                Value = 14m,
                ProductType = ValueObjects.ProductType.MerchandiseForResale,
                Active = true
            };

            var client = new Client
            {
                Name = "Fiora",
                Cep = "80000000",
                City = "Demacia",
                State = "DD",
                PhoneNumber = "40028922"
            };

            var clientList = new List<Client>
            {
                new Client
                {
                Name = "Sonic The Hedgehog",
                Cep = "80020330",
                City = "Tokyo",
                State = "RS",
                PhoneNumber = "40028922"
                },
                new Client
                {
                Name = "Ichigo Kurosaki",
                Cep = "80240180",
                City = "Soul Society",
                State = "SS",
                PhoneNumber = "40008002"
                }
            };

            using var db = new Data.ApplicationContext();
            db.Set<Client>().AddRange(clientList);  //List
            db.AddRange(client, product); 

            var records = db.SaveChanges();
            Console.WriteLine($"Total saved record(s): {records}");
        }
    }
}