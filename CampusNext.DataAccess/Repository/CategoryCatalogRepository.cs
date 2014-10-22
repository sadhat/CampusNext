using System.Threading.Tasks;
using CampusNext.AzureSearch.Repository;
using CampusNext.Entity;

namespace CampusNext.DataAccess.Repository
{
    public class CategoryCatalogRepository
    {
        public async Task<CategoryCatalog> All(string campusCode)
        {
            IAzureSearchRepository azureSearchTextbookRepository = new AzureSearchTextbookRepository();
            var categoryCatalog = new CategoryCatalog
            {
                CategoryTextbook = new CategoryInfo
                {
                    Id = 1,
                    Name = "Textbook",
                    Count = await azureSearchTextbookRepository.Count("textbook", campusCode)
                }
            };

            return categoryCatalog;
        }
    }
}
