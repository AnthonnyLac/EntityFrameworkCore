using EntityFrameworkCore.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCore.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public DateTime StartedIn { get; set; }
        public DateTime EndIn { get; set; }
        public TypeFreight TypeFreigth { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string observation { get; set; }

        public ICollection<OrderItem> items { get; set; }

    }
}
