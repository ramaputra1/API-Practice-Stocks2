using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(); // hanya bisa pakai ini
        // tambah ini semua
        Task<Stock?> GetByIdAsync(int id); // we always need id right, and we need firstdefault cant be null so we use that "?"
        Task<Stock> CreateAsync(Stock stockModel); // stock entity kita
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto);
        Task<Stock?> DeleteAsync(int id);



    }
}