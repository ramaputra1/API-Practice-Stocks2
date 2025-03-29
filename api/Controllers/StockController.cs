
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; // Untuk Async kamu
using api.Data; // Untuk import DBContext
using api.Dtos.Stock;
using api.Interfaces;
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
        private readonly IStockRepository _stockRepo; // bro _ nandakan apa?
        public StockController(ApplicationDBContext context, IStockRepository stockRepo) 
        {
            _stockRepo = stockRepo;
            _context = context; 
        }

        [HttpGet] // get (Read) ke api/stock
        public async Task<IActionResult> GetAll() // Jadikan async yaa disini(yanh beruhubungan ke DB)
        {
            // var stocks = await _context.Stocks.ToListAsync();  // eyo kita ganti ini jadi ngambil dari Repo ya
            var stocks = await _stockRepo.GetAllAsync(); // baca line atas
            var stockDto = stocks.Select(s => s.ToStockDto()); // disini juga jadi dipisah ke dto sendiri biar ga bareng asynv
            return Ok(stocks);
        }

        [HttpGet("{id}")] // + id
        public async Task<IActionResult> GetById([FromRoute] int id) // jadi async juga, dan jangan lupa di wrap di Task<..>
        {
            var stock = await _stockRepo.GetByIdAsync(id); // Find = built in function to search // search by PK || karena ini DB buat async jadi + await dan + FindAsync

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
            // gadipake lagi: (diganti sama repo)
            // await _context.Stocks.AddAsync(stockModel); // async
            // await _context.SaveChangesAsync(); // async
            // // semua function nya nanti jadi tambah async belakang nya dan itu built in allhamdulilah
            await _stockRepo.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        // Put (Update) --------------->
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            // var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id); // diganti repo semua kan yg DB _context
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
            if (stockModel == null)
            {
                return NotFound();
            }
            // diganti repo
            // stockModel.Symbol = updateDto.Symbol;
            // stockModel.CompanyName = updateDto.CompanyName;
            // stockModel.Purchase = updateDto.Purchase;
            // stockModel.LastDiv = updateDto.LastDiv;
            // stockModel.Industry = updateDto.Industry;
            // stockModel.MarketCap = updateDto.MarketCap;

            // await _context.SaveChangesAsync(); // asy c
            return Ok(stockModel.ToStockDto());
        }

        // Delete ---------------->
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);// async
            var stockModel = await _stockRepo.DeleteAsync(id);

            if(stockModel == null)
            {
                return NotFound();
            }

            // _context.Stocks.Remove(stockModel); 
            // _context.SaveChanges();
            return NoContent(); // ini adalah status Sukses untuk delete ya gaes
        }
    }
}