using System.Data;
using System.Data.SqlClient;

namespace Ticketing_Systems.Data
{
    public class SQL
    {
        private readonly string connectionString;

        public SQL(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public DataSet ExecuteProcedure(string procedureName)
        {
            DataSet ds = new DataSet();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

        public DataSet ExecuteProcedure(string procedureName, List<SqlParameter> parameters)
        {
            DataSet ds = new DataSet();

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    using (var adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(ds);
                    }
                }
            }

            return ds;
        }

    }
}
