using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppFinanse.Models
{
    public class Currency
    {
        public Guid Id { get; set; }//уникальный идентификатор
        public string Code { get; set; }//код валюты 
        public string Name { get; set; }//название валюты
    }
}
