
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Untuk Async kamu
using api.Data; // Untuk import DBContext
using api.Dtos.Stock;
using api.Mappers;
using api.Models; // Import Stock dari Models
using Microsoft.AspNetCore.Http.HttpResults; // Tipe hasil (ga terlalu dipake)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Untuk API control mu seperti HttpGet

namespace api.Controllers
{
    [Route("api/stock")] 
    [ApiController] 
    public class StockController : ControllerBase 
    {
        private readonly ApplicationDBContext _context; 
        public StockController(ApplicationDBContext context) 
        {
            _context = context; 
        }

        [HttpGet] // get (Read) ke api/stock
        public async Task<IActionResult> GetAll() // Jadikan async yaa disini(yanh beruhubungan ke DB)
        {
            var stocks = await _context.Stocks.ToListAsync(); // ada "await" disana untuk nandai disini asyn ya
            var stockDto = stocks.Select(s => s.ToStockDto()); // disini juga jadi dipisah ke dto sendiri biar ga bareng asynv
            return Ok(stocks);
        }

        [HttpGet("{id}")] // + id
        public async Task<IActionResult> GetById([FromRoute] int id) // jadi async juga, dan jangan lupa di wrap di Task<..>
        {
            var stock = await _context.Stocks.FindAsync(id); // Find = built in function to search // search by PK || karena ini DB buat async jadi + await dan + FindAsync

            if (stock == null)
            {
                return NotFound(); // return error
            }

            return Ok(stock.ToStockDto()); // return succes // dan response ToStockDto mu
        }

        // POST kitaaaa: ------------------------->

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            await _context.Stocks.AddAsync(stockModel); // async
            await _context.SaveChangesAsync(); // async
            // semua function nya nanti jadi tambah async belakang nya dan itu built in allhamdulilah
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        // Put (Update) --------------->
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id); // searching function untuk pastiike data yang mau di update exist ga

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

            await _context.SaveChangesAsync(); // asy c
            return Ok(stockModel.ToStockDto());
        }

        // Delete ---------------->
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);// async

            if(stockModel == null)
            {
                return NotFound();
            }

            _context.Stocks.Remove(stockModel); // tidak async karena gatau, REMOVE gapernah pake async
            _context.SaveChanges();
            return NoContent(); // ini adalah status Sukses untuk delete ya gaes
        }
    }
}