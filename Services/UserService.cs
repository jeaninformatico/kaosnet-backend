using Api_Kaos_Net.Data;
using Api_Kaos_Net.DTOs;
using Api_Kaos_Net.Interfaces;
using Api_Kaos_Net.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Kaos_Net.Services
{
    public class UserService : IUserService
    {
        private readonly KaosnetDbContext _context;

        public UserService(KaosnetDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    SessionTime = u.SessionTime,
                    FailedLogin = u.FailedLogin,
                    CurrentSessions = u.CurrentSessions,
                    Email = u.Email,
                    SecurityQuestion = u.SecurityQuestion,
                    SecretAnswer = u.SecretAnswer,
                    RoleFk = u.RoleFk,
                    UserStatus = u.UserStatus,
                    IsActive = u.IsActive == 1,
                    Idsession = u.Idsession
                }).ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var u = await _context.Users.FindAsync(id);
            if (u == null) return null;

            return new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                SessionTime = u.SessionTime,
                FailedLogin = u.FailedLogin,
                CurrentSessions = u.CurrentSessions,
                Email = u.Email,
                SecurityQuestion = u.SecurityQuestion,
                SecretAnswer = u.SecretAnswer,
                RoleFk = u.RoleFk,
                UserStatus = u.UserStatus,
                IsActive = u.IsActive == 1,
                Idsession = u.Idsession
            };
        }

        public async Task<UserDto> CreateAsync(UserDto dto)
        {
            var entity = new User
            {
                UserName = dto.UserName,
                SessionTime = dto.SessionTime,
                FailedLogin = dto.FailedLogin,
                CurrentSessions = dto.CurrentSessions,
                Email = dto.Email,
                SecurityQuestion = dto.SecurityQuestion,
                SecretAnswer = dto.SecretAnswer,
                RoleFk = dto.RoleFk,
                UserStatus = dto.UserStatus,
                IsActive = (sbyte)(dto.IsActive ? 1 : 0),
                Idsession = dto.Idsession
            };

            _context.Users.Add(entity);
            await _context.SaveChangesAsync();

            dto.UserId = entity.UserId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, UserDto dto)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null) return false;

            entity.UserName = dto.UserName;
            entity.SessionTime = dto.SessionTime;
            entity.FailedLogin = dto.FailedLogin;
            entity.CurrentSessions = dto.CurrentSessions;
            entity.Email = dto.Email;
            entity.SecurityQuestion = dto.SecurityQuestion;
            entity.SecretAnswer = dto.SecretAnswer;
            entity.RoleFk = dto.RoleFk;
            entity.UserStatus = dto.UserStatus;
            entity.IsActive = (sbyte)(dto.IsActive ? 1 : 0);
            entity.Idsession = dto.Idsession;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null) return false;

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto?> LoginAsync(string email, string secretAnswer)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.SecretAnswer == secretAnswer && u.IsActive == 1);

            if (user == null) return null;

            return new UserDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                RoleFk = user.RoleFk,
                IsActive = user.IsActive == 1,
                UserStatus = user.UserStatus
            };
        }
    }
}
