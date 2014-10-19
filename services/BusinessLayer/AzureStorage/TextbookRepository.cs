using System;
using System.Linq;
using CampusNext.Entity;
using CampusNext.Services.Entity;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


namespace CampusNext.Services.BusinessLayer.AzureStorage
{
    public class TextbookRepository : ITextbookRepository
    {
        public void Add(Textbook textbook)
        {
            // Retrieve the storage account from the connection string.
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            var table = tableClient.GetTableReference("Textbook");

            var newTextbook = new TextbookEntity(textbook.CampusCode, Guid.NewGuid())
            {
                Description = textbook.Description,
                Isbn = textbook.Isbn,
                Price = textbook.Price,
                Title = textbook.Name,
                CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow
            };

            var insertOperation = TableOperation.Insert(newTextbook);
            table.Execute(insertOperation);
        }

        public IQueryable<Textbook> All(TextbookSearchOption searchOptionOption)
        {
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            var table = tableClient.GetTableReference("textbook");
            

            // Construct the query operation for all customer entities where PartitionKey="NDSU".
            var query = new TableQuery<TextbookEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "NDSU"));

            var result = (from book in table.CreateQuery<TextbookEntity>()
                where book.PartitionKey == "NDSU"
                select book).Take(10);

            var items = result.ToList()
                    .ConvertAll(
                        entity =>
                            new Textbook
                            {
                                Id = Int32.Parse(entity.RowKey),
                                Description = entity.Description,
                                Isbn = entity.Isbn,
                                Price = entity.Price,
                                Name = entity.Title,
                                CampusCode = entity.CampusName
                            });

            return items.AsQueryable();
        } 
    }
}