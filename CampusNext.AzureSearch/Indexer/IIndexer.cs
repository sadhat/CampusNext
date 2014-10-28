using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusNext.AzureSearch.Indexer
{
    public interface IIndexer
    {
        void Create();
        void Update();
        bool Delete();
    }
}
