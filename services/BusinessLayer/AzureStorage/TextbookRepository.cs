﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using CampusNext.Services.Entity;
using CampusNext.Services.Models;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


namespace CampusNext.Services.BusinessLayer.AzureStorage
{
    public class TextbookRepository : ITextbookRepository
    {

        public TextbookRepository()
        {
            //Seed();
        }

        
        public void Add(Textbook textbook)
        {
            // Retrieve the storage account from the connection string.
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            var table = tableClient.GetTableReference("Textbook");

            var newTextbook = new TextbookEntity(textbook.CampusName, Guid.NewGuid())
            {
                Description = textbook.Description,
                Isbn = textbook.Isbn,
                Price = textbook.Price,
                Title = textbook.Title
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

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            var query = new TableQuery<TextbookEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "NDSU"));


            var items =
                table.ExecuteQuery(query)
                    .Select(entity => entity)
                    .ToList()
                    .ConvertAll(
                        (a) =>
                            new Textbook()
                            {
                                Id = Guid.Parse(a.RowKey),
                                Description = a.Description,
                                Isbn = a.Isbn,
                                Price = a.Price,
                                Title = a.Title,
                                CampusName = a.CampusName
                            });

            return items.AsQueryable();
        } 
    }
}