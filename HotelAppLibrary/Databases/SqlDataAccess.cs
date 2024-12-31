using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace HotelAppLibrary.Databases
{
    /// <summary>
    /// Talk with Database. Provides a data access layer for executing SQL queries or stored procedures.
    /// Uses Dapper for object-relational mapping (ORM).
    /// </summary>
    public class SqlDataAccess : ISqlDataAccess
    {
        // Stores configuration settings, including connection strings.
        private readonly IConfiguration _config;
        

        /// <summary>
        /// Initializes a new instance of SqlDataAccess with dependency injection for configuration.
        /// </summary>
        /// <param name="config">Configuration object containing connection string details.</param>
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }


        /// <summary>
        /// Executes an SQL query or stored procedure and returns a list of mapped results.
        /// </summary>
        /// <typeparam name="T">The type to map the query results to.</typeparam>
        /// <typeparam name="U">The type of the parameters passed to the query.</typeparam>
        /// <param name="sqlStatement">SQL query string or stored procedure name.</param>
        /// <param name="parameters">Parameters required for the SQL query or stored procedure.</param>
        /// <param name="connectionStringName">Name of the connection string in the configuration file.</param>
        /// <param name="options">Dynamic options (e.g., IsStoredProcedure to specify command type).</param>
        /// <returns>List of type T containing the query results.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the specified connection string is not found.</exception>
        public List<T> LoadData<T, U>(string sqlStatement,
                                      U parameters,
                                      string connectionStringName,
                                      bool isStoredProcedure = false)
        {
            // Retrieve the connection string from the configuration
            string connectionString = _config.GetConnectionString(connectionStringName)
                ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' was not found.");

            // Set default command type to plain SQL query
            CommandType commandType = CommandType.Text;

            // If options specify a stored procedure, adjust command type
            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            // Open a database connection and execute the query using Dapper
            using (IDbConnection connection = new SqlConnection(connectionString)) // Polymorphism 
            {
                // Execute the query and map results to a list of type T
                List<T> rows = connection.Query<T>(sqlStatement, parameters, commandType: commandType).ToList();

                return rows;
            }
        }


        /// <summary>
        /// Executes an SQL query or stored procedure to perform data insertion, update, or deletion.
        /// </summary>
        /// <typeparam name="T">The type of the parameters passed to the query.</typeparam>
        /// <param name="sqlStatement">SQL query string or stored procedure name.</param>
        /// <param name="parameters">Parameters required for the SQL query or stored procedure.</param>
        /// <param name="connectionStringName">Name of the connection string in the configuration file.</param>
        /// <param name="options">Dynamic options (e.g., IsStoredProcedure to specify command type).</param>
        /// <exception cref="InvalidOperationException">Thrown when the specified connection string is not found.</exception>
        public void SaveData<T>(string sqlStatement,
                                  T parameters,
                                  string connectionStringName,
                                  bool isStoredProcedure = false)
        {
            string connectionString = _config.GetConnectionString(connectionStringName)
                ?? throw new InvalidOperationException($"Connection string '{connectionStringName}' was not found.");

            CommandType commandType = CommandType.Text;

            if (isStoredProcedure == true)
            {
                commandType = CommandType.StoredProcedure;
            }

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlStatement, parameters, commandType: commandType);
            }
        }
    }
}
