using Api_Kaos_Net.DTOs;

namespace Api_Kaos_Net.Interfaces
{
    public interface IConversionRateService
    {
        Task<IEnumerable<ConversionRateDto>> GetAllAsync();
        Task<ConversionRateDto?> GetByIdAsync(int id);
        Task<ConversionRateDto> CreateAsync(ConversionRateDto dto);
        Task<bool> UpdateAsync(int id, ConversionRateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
