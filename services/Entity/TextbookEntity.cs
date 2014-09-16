using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CampusNext.Services.Models;
using Microsoft.WindowsAzure.Storage.Table;

namespace CampusNext.Services.Entity
{
    public class TextbookEntity : TableEntity
    {
        public TextbookEntity(string campusName)
        {
            this.PartitionKey = campusName;
            this.RowKey = Guid.NewGuid().ToString();
        }

        public TextbookEntity()
        {
            
        }
        public string Title { get; set; }
        public string Isbn { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}