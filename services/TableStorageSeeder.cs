using System;
using CampusNext.Services.Entity;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace CampusNext.Services
{
    public static class TableStorageSeeder
    {
        public static void Seed()
        {
            SeedTextbook();
        }

        private static void SeedTextbook()
        {
            // Retrieve the storage account from the connection string.
            var storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            var tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            var table = tableClient.GetTableReference("Textbook");
            table.CreateIfNotExists();

            // Construct the query operation for all customer entities where PartitionKey="Smith".
            var query = new TableQuery<TextbookEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "NDSU"));
            table.ExecuteQuery(query).ForEach(delegate(TextbookEntity entity)
            {
                var retrieveOperation = TableOperation.Retrieve<TextbookEntity>(entity.PartitionKey, entity.RowKey);
                var tableResult = table.Execute(retrieveOperation);
                var deleteOperation = TableOperation.Delete((TextbookEntity) tableResult.Result);
                table.Execute(deleteOperation);
            });


            // Create the batch operation.
            var batchOperation = new TableBatchOperation();
            //1
            var textbook1 = new TextbookEntity("NDSU", Guid.NewGuid())
            {
                Description =
                    "When you have a question about C# 5.0 or the .NET CLR, this bestselling guide has precisely the answers you need. Uniquely organized around concepts and use cases, this updated fifth edition features a reorganized section on concurrency, threading, and parallel programming—including in-depth coverage of C# 5.0’s new asynchronous functions.",
                Isbn = "978-1449320102",
                Price = 34.33,
                Title = "C# 5.0 in a Nutshell: The Definitive Reference"
            };

            //2
            var textbook2 = new TextbookEntity("NDSU", Guid.NewGuid())
            {
                Description =
                    "This new edition of this invaluable reference brings the coverage of software metrics fully up to date. The book has been rewritten and redesigned to take into account the fast changing developments in software metrics, most notably their widespread penetration into industrial practice. New sections deal with prcess maturity and measurement, goal-question-metric, metrics plans, experimentation, empirical studies, object-oriented metrics, and metrics tools. 88 line illustrations.",
                Isbn = "978-0534956004",
                Price = 44.99,
                Title = "Software Metrics"
            };

            //3
            var textbook3 = new TextbookEntity("NDSU", Guid.NewGuid())
            {
                Description =
                    "The #1 Easy, Commonsense Guide to Database Design! Michael J. Hernandez’s best-selling Database Design for Mere Mortals® has earned worldwide respect as the clearest, simplest way to learn relational database design. Now, he’s made this hands-on, software-independent tutorial even easier, while ensuring that his design methodology is still relevant to the latest databases, applications, and best practices. Step by step, Database Design for Mere Mortals ® , Third Edition, shows you how to design databases that are soundly structured, reliable, and flexible, even in modern web applications. Hernandez guides you through everything from database planning to defining tables, fields, keys, table relationships, business rules, and views. You’ll learn practical ways to improve data integrity, how to avoid common mistakes, and when to break the rules.",
                Isbn = "978-0534956004",
                Price = 23.99,
                Title = "Database Design for Mere Mortals: A Hands-On Guide to Relational Database Design (3rd Edition)"
            };

            //4
            var textbook4 = new TextbookEntity("NDSU", Guid.NewGuid())
            {
                Description =
                    "Most programming languages contain good and bad parts, but JavaScript has more than its share of the bad, having been developed and released in a hurry before it could be refined. This authoritative book scrapes away these bad features to reveal a subset of JavaScript that's more reliable, readable, and maintainable than the language as a whole—a subset you can use to create truly extensible and efficient code.",
                Isbn = "978-0596517748",
                Price = 17.20,
                Title = "JavaScript: The Good Parts"
            };

            //5
            var textbook5 = new TextbookEntity("NDSU", Guid.NewGuid())
            {
                Description =
                    "Single Page Web Applications shows how your team can easily design, test, maintain, and extend sophisticated SPAs using JavaScript end-to-end, without getting locked into a framework. Along the way, you'll develop advanced HTML5, CSS3, and JavaScript skills, and use JavaScript as the language of the web server and the database. This book assumes basic knowledge of web development. No experience with SPAs is required. Purchase of the print book includes a free eBook in PDF, Kindle, and ePub formats from Manning Publications.",
                Isbn = "ISBN: 978-0596517748",
                Price = 35.20,
                Title = "Single Page Web Applications: JavaScript end-to-end"
            };

            batchOperation.Insert(textbook1);
            batchOperation.Insert(textbook2);
            batchOperation.Insert(textbook3);
            batchOperation.Insert(textbook4);
            batchOperation.Insert(textbook5);

            table.ExecuteBatch(batchOperation);
        }

    }
}