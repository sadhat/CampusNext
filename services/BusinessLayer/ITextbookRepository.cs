using System.Linq;
using CampusNext.Services.Models;

namespace CampusNext.Services.BusinessLayer
{
    public interface ITextbookRepository
    {
        IQueryable<Textbook> All(TextbookSearchOption searchOptionOption);
    }
}