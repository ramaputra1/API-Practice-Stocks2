using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class StockDto
    {
        // Copas dari models (stock)
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty; 
        public decimal Purchase { get; set;}  
        public decimal LastDiv { get; set;}
        public string Industry { get; set;} = string.Empty;
        public long MarketCap { get; set;} 
        // Disini harus nya Comments, tapi kita emoh 
    }
}