using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models; // Import sesmua models
using Microsoft.EntityFrameworkCore; // Library dari EF untuk akses database class entity mu nanti

namespace api.Data
{
    public class ApplicationDBContext : DbContext // ada ":" itu inherit
    // Jadi "ApplicationDBContext" itu kelas ku, yang mewarisi "DbContext" milik EF untuk:
    // atir koneksi database, melacak perubahan data, dan jalankan SQL lewat model (lakukan imigrasi)
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) // Constructor (Nama sama dengan Kelas) // dan paramter nya yang Db itu Tipe data nya ya
        : base(dbContextOptions) // Menerima parameter "DbContextOptions" untuk dilempar ke parameter ke constructor DBContext // : itu super atau inherit darisitu harus nurunin itu
        // tujuan: tahu database mana yan gmau dihubungkan
        {
            
        }

        // Properties: (Mewakili TABLE di database nanti) ketika di run update
        public DbSet<Stock> Stocks {get; set;}
        public DbSet<Comment> Comments { get; set;}
    }
}

// Constructor:
// Ternyata wajib nurunin Constructor dari parent ke anak juga, jadi kenapa ada ": base(dbCon..)" itu
// Inherit juga wajib nurunin constructor nya seperti interface

// !!!: Hanya wajib jika parent mu punya PARAMETER, harus di turunin