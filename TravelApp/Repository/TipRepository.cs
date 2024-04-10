using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelApp.Data;
using TravelApp.Interfaces;
using TravelApp.Models;

namespace TravelApp.Repository
{
    public class TipRepository : ITipRepository
    {
        private readonly ApplicationDbContext _context;

        public TipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tip>> GetTipsForPlace(int placeId)
        {
            return await _context.Tips.Where(t => t.PlaceId == placeId).ToListAsync();
        }

        public async Task Add(Tip tip)
        {
            _context.Tips.Add(tip);
            await _context.SaveChangesAsync();
        }

        public async Task<Tip> GetByIdAsync(int id)
        {
            return await _context.Tips.FindAsync(id);
        }

        public async Task Delete(int id)
        {
            var tip = await GetByIdAsync(id);
            if (tip != null)
            {
                _context.Tips.Remove(tip);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Tip tip)
        {
            _context.Entry(tip).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
