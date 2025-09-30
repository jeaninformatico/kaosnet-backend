using Api_Kaos_Net.DTO;

namespace Api_Kaos_Net.Interfaces
{
    public interface IRoleAccessService
    {
        Task<IEnumerable<RoleAccessDto>> GetAllAsync();
        Task<RoleAccessDto?> GetByIdAsync(int id);
        Task<RoleAccessDto> CreateAsync(RoleAccessDto dto);
        Task<bool> UpdateAsync(int id, RoleAccessDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
