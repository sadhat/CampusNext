using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CampusNext.Entity;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace CampusNext.Services.BusinessLayer.DocumentDb
{
    public class TextbookRepository : ITextbookRepository
    {
        private static readonly string EndpointUrl = ConfigurationManager.AppSettings["DocumentDbEndpoint"];
        private static readonly string AuthorizationKey = ConfigurationManager.AppSettings["DocumentDbAuthorizationKey"];

        static readonly DocumentClient Client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);


        public IQueryable<Textbook> All(TextbookSearchOption searchOptionOption)
        {
            var database = GetOrCreateDatabaseAsync("Textbook").Result;
            var documentCollection =
                GetOrCreateCollectionAsync(database.SelfLink, "BookCollection").Result;

            var textbooks =
                from t in
                    Client.CreateDocumentQuery<Textbook>(documentCollection.SelfLink,
                        new FeedOptions {EnableScanInQuery = true})
                where t.Name.StartsWith("Java")
                select t;
                

            return textbooks.ToList().AsQueryable();
        }

        public void Add(Textbook textbook)
        {
            var database = GetOrCreateDatabaseAsync("Textbook").Result;
            var documentCollection =
                GetOrCreateCollectionAsync(database.SelfLink, "BookCollection").Result;
            Client.CreateDocumentAsync(documentCollection.SelfLink, textbook);
        }

        /// <summary>
        /// Get a Database by id, or create a new one if one with the id provided doesn't exist.
        /// </summary>
        /// <param name="id">The id of the Database to search for, or create.</param>
        /// <returns>The matched, or created, Database object</returns>
        private static async Task<Database> GetOrCreateDatabaseAsync(string id)
        {
            Database database = Client.CreateDatabaseQuery().Where(db => db.Id == id).ToArray().FirstOrDefault();
            if (database == null)
            {
                database = await Client.CreateDatabaseAsync(new Database { Id = id });
            }

            return database;
        }

        /// <summary>
        /// Get a DocuemntCollection by id, or create a new one if one with the id provided doesn't exist.
        /// </summary>
        /// <param name="id">The id of the DocumentCollection to search for, or create.</param>
        /// <returns>The matched, or created, DocumentCollection object</returns>
        private static async Task<DocumentCollection> GetOrCreateCollectionAsync(string dbLink, string id)
        {
            DocumentCollection collection = Client.CreateDocumentCollectionQuery(dbLink).Where(c => c.Id == id).ToArray().FirstOrDefault();
            if (collection == null)
            {
                collection = await Client.CreateDocumentCollectionAsync(dbLink, new DocumentCollection { Id = id });
            }

            return collection;
        }
    }
}