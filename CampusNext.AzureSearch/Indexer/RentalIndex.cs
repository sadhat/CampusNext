using System.Collections.Generic;

namespace CampusNext.AzureSearch.Indexer
{
    public class RentalIndex : IndexerBase
    {
        public RentalIndex(string serviceName, string serviceApiKey)
            : base(serviceName, serviceApiKey, "rental") {}

        protected override List<dynamic> GetFieldDefinition()
        {
            return new List<dynamic>
            {
                new
                {
                    Name = "id",
                    Type = "Edm.String",
                    Key = true,
                    Searchable = false,
                    Filterable = false,
                    Sortable = false,
                    Facetable = false,
                    Retrievable = true,
                    Suggestions = false
                },
                new
                {
                    Name = "address",
                    Type = "Edm.String",
                    Key = false,
                    Searchable = true,
                    Filterable = true,
                    Sortable = true,
                    Facetable = true,
                    Retrievable = true,
                    Suggestions = true
                },
                new
                {
                    Name = "rent",
                    Type = "Edm.String",
                    Key = false,
                    Searchable = true,
                    Filterable = true,
                    Sortable = true,
                    Facetable = true,
                    Retrievable = true,
                    Suggestions = false
                },
                new
                {
                    Name = "room",
                    Type = "Edm.Int32",
                    Key = false,
                    Searchable = false,
                    Filterable = true,
                    Sortable = false,
                    Facetable = true,
                    Retrievable = true,
                    Suggestions = false
                },
                new
                {
                    Name = "description",
                    Type = "Edm.String",
                    Key = false,
                    Searchable = true,
                    Filterable = true,
                    Sortable = false,
                    Facetable = true,
                    Retrievable = true,
                    Suggestions = false
                },
                new
                {
                    Name = "updatedDate",
                    Type = "Edm.DateTimeOffset",
                    Key = false,
                    Searchable = false,
                    Filterable = true,
                    Sortable = false,
                    Facetable = false,
                    Retrievable = false,
                    Suggestions = false
                },
                new
                {
                    Name = "createdDate",
                    Type = "Edm.DateTimeOffset",
                    Key = false,
                    Searchable = false,
                    Filterable = true,
                    Sortable = false,
                    Facetable = false,
                    Retrievable = false,
                    Suggestions = false
                },
                new
                {
                    Name = "campusCode",
                    Type = "Edm.String",
                    Key = false,
                    Searchable = true,
                    Filterable = true,
                    Sortable = false,
                    Facetable = true,
                    Retrievable = true,
                    Suggestions = true
                },
            };
        }
    }
}
