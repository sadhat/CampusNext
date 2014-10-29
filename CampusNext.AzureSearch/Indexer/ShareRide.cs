using System.Collections.Generic;

namespace CampusNext.AzureSearch.Indexer
{
    public class ShareRideIndex : IndexerBase
    {
        public ShareRideIndex(string serviceName, string serviceApiKey)
            : base(serviceName, serviceApiKey, "shareride") {}

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
                    Name = "fromLocation",
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
                    Name = "toLocation",
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
                    Name = "isRoundtrip",
                    Type = "Edm.Boolean",
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
                    Name = "startDateTime",
                    Type = "Edm.DateTimeOffset",
                    Key = false,
                    Searchable = false,
                    Filterable = true,
                    Sortable = false,
                    Facetable = false,
                    Retrievable = true,
                    Suggestions = false
                },
                new
                {
                    Name = "returnDateTime",
                    Type = "Edm.DateTimeOffset",
                    Key = false,
                    Searchable = false,
                    Filterable = true,
                    Sortable = false,
                    Facetable = false,
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
