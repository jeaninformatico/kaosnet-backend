using Api_Kaos_Net.DTO;

namespace Api_Kaos_Net.Interfaces
{
    public interface ISubscriptionPlanAccountService
    {
        Task<IEnumerable<SubscriptionPlanAccountDto>> GetAllAsync();
        Task<SubscriptionPlanAccountDto?> GetByIdAsync(int id);
        Task<SubscriptionPlanAccountDto> CreateAsync(SubscriptionPlanAccountDto dto);
        Task<bool> UpdateAsync(int id, SubscriptionPlanAccountDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
