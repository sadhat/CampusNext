using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace CampusNext.Services.Entity
{
    public class TextbookEntity : TableEntity
    {
        public TextbookEntity(string campusName, Guid id)
        {
            PartitionKey = CampusName = campusName;
            Id = id;
            RowKey = Id.ToString();
        }

        public TextbookEntity()
        {
            
        }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Guid Id { get; private set; }
        public string CampusName { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}