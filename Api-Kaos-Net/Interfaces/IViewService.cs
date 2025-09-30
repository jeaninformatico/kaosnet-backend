using Api_Kaos_Net.DTO;

namespace Api_Kaos_Net.Interfaces
{
    public interface IViewService
    {
        Task<IEnumerable<ViewDto>> GetAllAsync();
        Task<ViewDto?> GetByIdAsync(int id);
        Task<ViewDto> CreateAsync(ViewDto dto);
        Task<bool> UpdateAsync(int id, ViewDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
