using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

// kita punya ini untuk jadi anak2 interface
namespace api.Repository
{
    public class StockRepository : IStockRepository // Pencet sesuatu pas masih merah lalu implements interfaces
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context) // bring our DBContext
        {
            _context = context;
        }
        public Task<List<Stock>> GetAllAsync()
        {
            return _context.Stocks.ToListAsync(); // dari controller pertama get
        }
    }
}