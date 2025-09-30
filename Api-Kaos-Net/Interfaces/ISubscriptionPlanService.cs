using Api_Kaos_Net.DTOs;

namespace Api_Kaos_Net.Interfaces
{
    public interface ISubscriptionPlanService
    {
        Task<IEnumerable<SubscriptionPlanDto>> GetAllAsync();
        Task<SubscriptionPlanDto?> GetByIdAsync(int id);
        Task<SubscriptionPlanDto> CreateAsync(SubscriptionPlanDto dto);
        Task<bool> UpdateAsync(int id, SubscriptionPlanDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
