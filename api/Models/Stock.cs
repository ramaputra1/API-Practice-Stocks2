using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema; // Penggunaan Tipe data column perlu ini.
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;


        [Column(TypeName = "decimal(18,2)")] 
        /*
         Column ini untuk apa? untuk manipulasi tipe decimal, agar bisa berbentuk (18,2)
         yang artinya: max 18 digit angka dan 2 dibelakang koma.
         Artinya column nya itu termanipulasi, kalau gapake ini nanti jadi sistem default dan kita gamau itu.
         */
        public decimal Purchase { get; set;}


        [Column(TypeName = "decimal(18,2)")]  
        public decimal LastDiv { get; set;}


        public string Industry { get; set;} = string.Empty;
        public long MarketCap { get; set;} 

        public List<Comment> Comments {get; set;} = new List<Comment>();
        /*
        Apa maksudnya?
        Pakai generik untuk menyimpan list dari Comments, jadi satu stock boleh punya banyak komen.
        Ketika mendefinisikan <Comment> artinya ini nanti akan berisi List Comment yang berisi komen 1,2,3 so on.

        Itu adalah keuntungan data Generik (<>)
        Untuk menyimpan sesuatu secara ter list banyak walaupun beda2 nantinya.


        Kenapa pakai = new List<Comment>();
        Agar setiap membuat objek komen baru nanti tidak akan error, karena default value nya sudah dibuat jadi object dengan "new" itu
        */
        
    }
}