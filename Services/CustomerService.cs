using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly KaosnetDbContext _context;

        public CustomerService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    ContactEmail = c.ContactEmail,
                    ContactNumber = c.ContactNumber,
                    SubscriptionPlanFk = c.SubscriptionPlanFk,
                    CustomerStatus = c.CustomerStatus,
                    IsActive = c.IsActive == 1,
                    Idsession = c.Idsession
                }).ToListAsync();
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var c = await _context.Customers.FindAsync(id);
            if (c == null) return null;

            return new CustomerDto
            {
                CustomerId = c.CustomerId,
                CustomerName = c.CustomerName,
                ContactEmail = c.ContactEmail,
                ContactNumber = c.ContactNumber,
                SubscriptionPlanFk = c.SubscriptionPlanFk,
                CustomerStatus = c.CustomerStatus,
                IsActive = c.IsActive == 1,
                Idsession = c.Idsession
            };
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto dto)
        {
            var entity = new Customer
            {
                CustomerName = dto.CustomerName,
                ContactEmail = dto.ContactEmail,
                ContactNumber = dto.ContactNumber,
                SubscriptionPlanFk = dto.SubscriptionPlanFk,
                CustomerStatus = dto.CustomerStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();

            dto.CustomerId = entity.CustomerId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, CustomerDto dto)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity == null) return false;

            entity.CustomerName = dto.CustomerName;
            entity.ContactEmail = dto.ContactEmail;
            entity.ContactNumber = dto.ContactNumber;
            entity.SubscriptionPlanFk = dto.SubscriptionPlanFk;
            entity.CustomerStatus = dto.CustomerStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Customers.FindAsync(id);
            if (entity == null) return false;

            _context.Customers.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
