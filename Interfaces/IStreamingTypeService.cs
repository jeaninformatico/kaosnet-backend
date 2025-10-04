using Api_Kaos_Net.DTOs;

namespace Api_Kaos_Net.Interfaces
{
    public interface IStreamingTypeService
    {
        Task<IEnumerable<StreamingTypeDto>> GetAllAsync();
        Task<StreamingTypeDto?> GetByIdAsync(int id);
        Task<StreamingTypeDto> CreateAsync(StreamingTypeDto dto);
        Task<bool> UpdateAsync(int id, StreamingTypeDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
