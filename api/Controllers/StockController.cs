
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Untuk Async kamu
using api.Data; // Untuk import DBContext
using api.Dtos.Stock;
using api.Mappers;
using api.Models; // Import Stock dari Models
using Microsoft.AspNetCore.Http.HttpResults; // Tipe hasil (ga terlalu dipake)
using Microsoft.AspNetCore.Mvc; // Untuk API control mu seperti HttpGet

namespace api.Controllers
{
    [Route("api/stock")] // EndPoint masuk ke api/stock
    [ApiController] // Fitur dasar
    public class StockController : ControllerBase // StockController: Nama Controller // ControlBase: turunan dari Controlbase untuk Web API
    {
        private readonly ApplicationDBContext _context; // "_context" adalah variable atau field digunakan dalam seluruh method di controller
        // ApplicatonDBContext: Nama kelas untuk mengatur koneksi dan operasi database
        // _context: field internal untuk simpan instance (?)
        public StockController(ApplicationDBContext context) // Constructor
        // Constructor menerima APplicationDBContext yg di inject oleh dependency injection
        {
            _context = context; // simpan field context
        }

        [HttpGet] // get (Read) ke api/stock
        public IActionResult GetAll() // IACTIONRESULT: interface yang memungkinkan kamu mengembalikan nilai Ok() atau NotFOund() Error seperti di ID bawah setelah ini
        {
            var stocks = _context.Stocks.ToList()
            .Select(s => s.ToStockDto()); // ambil semua data dari tabel stocks dan convert ke list
            // Bro disini ada .Select itu nanti yang akan memberi repsonse dari semua select dari Dtos
            return Ok(stocks);
        }

        [HttpGet("{id}")] // + id
        public IActionResult GetById([FromRoute] int id) // FromRoute: mengambil nilai ID dari URL
        {
            var stock = _context.Stocks.Find(id); // Find = built in function to search // search by PK

            if (stock == null)
            {
                return NotFound(); // return error
            }

            return Ok(stock.ToStockDto()); // return succes // dan response ToStockDto mu
        }

        // POST kitaaaa: ------------------------->

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stocks.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        // Put (Update) --------------->
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id); // searching function untuk pastiike data yang mau di update exist ga

            if (stockModel == null)
            {
                return NotFound();
            }
            // belum pasti ini apa, mungkin apa yang diupdate untuk masuk ke database
            stockModel.Symbol = updateDto.Symbol;
            stockModel.CompanyName = updateDto.CompanyName;
            stockModel.Purchase = updateDto.Purchase;
            stockModel.LastDiv = updateDto.LastDiv;
            stockModel.Industry = updateDto.Industry;
            stockModel.MarketCap = updateDto.MarketCap;

            _context.SaveChanges();
            return Ok(stockModel.ToStockDto());
        }

        // Delete ---------------->
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

            if(stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel);
            _context.SaveChanges();
            return NoContent(); // ini adalah status Sukses untuk delete ya gaes
        }
    }
}