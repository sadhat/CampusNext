using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CampusNext.DataAccess.Repository
{
    public abstract class RepositoryBase
    {
        protected IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["CampusNextContext"].ConnectionString);
            }
        }

        protected IDbConnection OpenConnection
        {
            get
            {
                var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["CampusNextContext"].ConnectionString);
                conn.Open();
                return conn;
            }
        }
    }
}