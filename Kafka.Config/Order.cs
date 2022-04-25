using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoC.Kafka.Shared
{
    public class Order
    {
        public Order(int id, string name, decimal amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
