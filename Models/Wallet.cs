using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppFinanse.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }//уникальный идентификатор
        public string Name { get; set; }
        public decimal Balance { get; set; }
        // Навигационное свойство для Currency
        public Currency Currency { get; set; }
        // Внешний ключ для Currency
        public Guid CurrencyId { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}