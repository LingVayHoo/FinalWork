﻿using AuthServer.Models;
using AuthServer.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace AuthServer.Services.RefreshTokenRepositories
{
    public class DataBaseRefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AuthenticationDbContext _context;

        public DataBaseRefreshTokenRepository(AuthenticationDbContext context)
        {
            _context = context;
        }

        public async Task Create(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken> GetByToken(string token)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }

        public async Task Delete(Guid id)
        {
            RefreshToken refreshToken = await _context.RefreshTokens.FindAsync(id);
            if (refreshToken != null) 
            { 
                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAll(Guid userId)
        {
            IEnumerable<RefreshToken> refreshTokens = await _context.RefreshTokens
                .Where(t => t.UserId == userId).ToListAsync();

            _context.RefreshTokens.RemoveRange(refreshTokens);
            await _context.SaveChangesAsync();
        }

        
    }
}
