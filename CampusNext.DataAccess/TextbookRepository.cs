using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CampusNext.DataAccess
{
    public class TextbookRepository
    {
        public async Task<List<Textbook>> GetTextbooksAsync(int userId)
        {
            using (var context = new CampusNextContext())
            {
                var result = await context.Textbooks.Where((t) => t.UserId == userId).ToListAsync();
                return result;
            }
        }

        public async Task AddAsync(Textbook textbook)
        {
            using (var context = new CampusNextContext())
            {
                context.Entry(textbook).State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(Textbook textbook)
        {
            using (var context = new CampusNextContext())
            {
                context.Entry(textbook).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Textbook textbook)
        {
            using (var context = new CampusNextContext())
            {
                context.Entry(textbook).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

    }
}