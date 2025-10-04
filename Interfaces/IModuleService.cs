using Api_Kaos_Net.DTO;

namespace Api_Kaos_Net.Interfaces
{
    public interface IModuleService
    {
        Task<IEnumerable<ModuleDto>> GetAllAsync();
        Task<ModuleDto?> GetByIdAsync(int id);
        Task<ModuleDto> CreateAsync(ModuleDto dto);
        Task<bool> UpdateAsync(int id, ModuleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
