/*
Dalam .NET
Model adalah sebuah class.

Class Comment adalah 'MODEL' untuk tabel Comment di database.(Lihat Catatan ttg tujuan model itu untuk apa lagi di Ipad)
*/

//-------------------->

// Otomatis ada mereka, karena ini adalah framework ya, jadi jika sewaktu2 butuh kita gaperlu import2 lagi.
using System; // Fungsi: Make you be able to use Basic Tipe data such as: Datetime, Console, Math, String, dll 
// Kalo gapakai ini nanti kamu gabisa nulis string gitu doang, harus nulis System.string karena di bahasa C# memang gitu

using System.Collections.Generic; // Fungsi: Berisi tipe koleksi Generik: List<T>, Dictionary<TKey, TValue>
using System.Linq; // Fungsi: Untuk melakukan query instruction ke data. (array, list, database) Cont: Where, Select dll
using System.Threading.Tasks; // Fungsi: Perlu untuk sistem Async dan Await

//-------------------->

namespace api.Models
/*
Ini adalah fitur di C# atau .NET tujuan untuk memberi tahu lokasi class ini:
lokasi nya ada di folder api -> Models jadi api.Models
tujuannya, jika project sudah besar, ga repot pusing nyari alamat nya. 
*/

//-------------------->


{
    public class Comment // Class Comment, Class ini adalah sebuah 'MODEL' yang mewakili tabel di database yang di anu API nanti
    {
        public int Id {get; set;} 
        /*
        Dalan .NET jika ada pakai "Id" maka otomatis akan jadi PK, dan akan terjadi juga jika pakai nama kelas + Id (CommentId).
        Bagaimana jika PK nya mau nama lain?
        Gini:

        using System.ComponentModel.DataAnnotations; // Pakai ini

        public class Comment
        {
            [Key]
            public int CommentPrimaryKey { get; set; }
            public string Title { get; set; } = string.Empty;
        }

        */

        public string Title {get; set;} = string.Empty; // Properti untuk title comment nanti
        public string Content {get; set;} = string.Empty; // Properti untuk title comment nanti
        public DateTime CreatedOn {get; set;} = DateTime.Now; // Ini otomatis akan menyimpan waktu saat komentar dibuat. Jika tidak diberikan value saat membuat object, otomatis akan terisi waktu saat object dibuat.
        public int? StockId {get; set;} 
        /*
        Ini adalah Foreign Key (FK).
        Menuju ke PK dari tabel Stock, agar ke link.
        *Tanda int? artinya nilai ini bisa nullable — boleh kosong. = Artinya, komentar boleh saja tidak terhubung ke Stock tertentu
        */
        public Stock? Stock {get; set;}
        /*
        Ini disebut navigation property.
        Navigation Property: Navigation property adalah property yang menghubungkan satu model ke model lain secara langsung.
        Navigation Property selalu ada jika terhubung dengan FK dari Model tabel lain.

        Jika nama property nya adalah kelas dari Model lain, otomatis itu Navigation Property,
        tapi jika mau untuk gapake itu bratti perlu kasi tahu dulu di import an.

        Untuk apa?

        Bayangkan:

        StockId = nomor identitas stock (misalnya: 3)
        Stock = kartu nama data legkap lengkapnya stock nomor 3.
        Kalau kamu punya StockId = 3, navigation property Stock memungkinkan kamu langsung ambil data stock lengkap dengan Id = 3, tanpa query manual, dan maniplasi itu.

        */
        
    }
}