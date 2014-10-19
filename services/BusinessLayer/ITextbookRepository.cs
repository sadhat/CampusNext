using System.Linq;
using CampusNext.Entity;

namespace CampusNext.Services.BusinessLayer
{
    public interface ITextbookRepository
    {
        IQueryable<Textbook> All(TextbookSearchOption searchOptionOption);
        void Add(Textbook textbook);
    }
}