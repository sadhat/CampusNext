using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using CampusNext.Entity;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace CampusNext.Services
{
    public class DocumentDbSeeder
    {
        private static readonly string EndpointUrl = ConfigurationManager.AppSettings["DocumentDbEndpoint"];
        private static readonly string AuthorizationKey = ConfigurationManager.AppSettings["DocumentDbAuthorizationKey"];

        static readonly DocumentClient Client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);

        public static void Seed()
        {
            var database =  GetOrCreateDatabaseAsync("Textbook");
            var collection =
                GetOrCreateCollectionAsync(database.Result.SelfLink, "BookCollection").Result;

            var a = CreateTextbookDocuments(collection.SelfLink);
            
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

        private static async Task CreateTextbookDocuments(string colSelfLink)
        {
            var textbook1 = new Textbook
            {
                Id = 1,
                Description =
                    "When you have a question about C# 5.0 or the .NET CLR, this bestselling guide has precisely the answers you need. Uniquely organized around concepts and use cases, this updated fifth edition features a reorganized section on concurrency, threading, and parallel programming—including in-depth coverage of C# 5.0’s new asynchronous functions.",
                Isbn = "978-1449320102",
                Price = 34.33,
                Name = "C# 5.0 in a Nutshell: The Definitive Reference",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            await Client.CreateDocumentAsync(colSelfLink, textbook1);

            var textbook2 = new Textbook
            {
                Id = 2,
                Description =
                    "This new edition of this invaluable reference brings the coverage of software metrics fully up to date. The book has been rewritten and redesigned to take into account the fast changing developments in software metrics, most notably their widespread penetration into industrial practice. New sections deal with prcess maturity and measurement, goal-question-metric, metrics plans, experimentation, empirical studies, object-oriented metrics, and metrics tools. 88 line illustrations.",
                Isbn = "978-0534956004",
                Price = 44.99,
                Name = "Software Metrics",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            await Client.CreateDocumentAsync(colSelfLink, textbook2);
        }
    }
}