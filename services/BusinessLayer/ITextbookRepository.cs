using System.Linq;
using System.Threading.Tasks;
using CampusNext.Services.Models;

namespace CampusNext.Services.BusinessLayer
{
    public interface ITextbookRepository
    {
        IQueryable<Textbook> All(TextbookSearchOption searchOptionOption);
        void Add(Textbook textbook);
    }
}