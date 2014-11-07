using System.Collections.Generic;
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
            IAzureSearchRepository azureSearchFindTutorRepository = new AzureSearchFindTutorRepository();
            IAzureSearchRepository azureSearchShareRiderRepository = new AzureSearchShareRideRepository();
            var rentalRepository = new RentalRepository();
            var tasks = new List<Task<int>> {
                azureSearchTextbookRepository.Count("textbook", campusCode), 
                azureSearchFindTutorRepository.Count("findtutor", campusCode),
                azureSearchShareRiderRepository.Count("shareride", campusCode),
                rentalRepository.CountAsync(campusCode)
            };
            var doneTasks = await Task.WhenAll(tasks);
            
            var countTextbook = doneTasks[0];
            var countTutor = doneTasks[1];
            var countShareRide = doneTasks[2];
            var rental = doneTasks[3];

            var categoryCatalog = new CategoryCatalog
            {
                CategoryTextbook = new CategoryInfo
                {
                    Id = 1,
                    Name = "textbook search",
                    Count = countTextbook
                },
                CategoryFindTutor = new CategoryInfo
                {
                    Id =2,
                    Name = "find tutor",
                    Count = countTutor
                },
                CategoryRoomForRent = new CategoryInfo
                {
                    Id = 3,
                    Name = "room for rent",
                    Count = rental
                },
                CategoryCampusLifeEvents = new CategoryInfo
                {
                    Id = 4,
                    Name = "campus life events",
                    Count = 0
                },
                CategoryShareRide = new CategoryInfo
                {
                    Id = 5,
                    Name = "share a ride",
                    Count = countShareRide
                },
                CategoryFindStudyGroup = new CategoryInfo
                {
                    Id = 6,
                    Name = "find study group",
                    Count = 0
                }
            };

            return categoryCatalog;
        }
    }
}
