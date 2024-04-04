using Aspose.Slides.SlideShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppFinanse.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }//индивидуальный идентификатор
        public DateTime Date { get; set; }//дата транзакции
        public decimal Amount { get; set; }//сумма транзакции
        public string Description { get; set; }
        public Guid AccountId { get; set; }
        public TransactionType Type { get; set; }//доход или расход
    }
    public enum TransactionType
    {
        Income,//доход
        Expense// расход
    }
}
