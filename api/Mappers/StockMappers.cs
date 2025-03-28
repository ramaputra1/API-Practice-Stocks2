using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock; // utk akses dtos stock
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Storage; // untuk akses Models

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockModel) // StockDto: Tipe data // ToStockDto: nama method mu // (this..): agar bikin method jadi extension method untuk object stock
        /* bro, kenapa static?
        static berarti "tidak bisa di inisiasi" dan kenapa pake static?
        agar lgsg milik kelas, jadi gaperlu bikin new setiap saat. kan kita disini emg mau kembaliin object kan makanya lgsg di static aja
        */
        {
            return new StockDto // masih bingung kenapa disini new padahal static
            {
                // Semua Values yang kamu mau tampil nanti (Feel Free to Trim anything)
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap
            };
        }


        // Punya POST RequestStock ------------>
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Purchase = stockDto.Purchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}