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
        public TextbookEntity(string campusName, Guid id)
        {
            this.PartitionKey = this.CampusName = campusName;
            this.Id = id;
            this.RowKey = Id.ToString();
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
    }
}