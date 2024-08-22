using System;
using System.Data.SqlClient;

namespace ColorBackend.DataAccess
{
    /// <summary>
    /// The <c>Connection</c> class is responsible for managing the connection to the SQL Server database.
    /// It retrieves the connection string from environment variables and provides methods to open and close connections.
    /// </summary>
    public class Connection
    {
        // The connection string for the database, initialized from environment variables.
        private readonly string? connectionString;

        /// <summary>
        /// Initializes a new instance of the <c>Connection</c> class.
        /// The connection string is constructed using environment variables for server, database, user, and password.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the connection string is null, indicating a configuration error.
        /// </exception>
        public Connection()
        {
            connectionString = $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
                               $"Database={Environment.GetEnvironmentVariable("DB_CATALOG")};" +
                               $"user id={Environment.GetEnvironmentVariable("DB_USER")}; " +
                               $"password={Environment.GetEnvironmentVariable("DB_PASSWORD")}; " +
                               $"Max Pool Size=10024";

            //connectionString = $"Server=DESKTOP-8AKMBMI\\SQLEXPRESS01;Database=ColorBackend;Trusted_Connection=True;Max Pool Size=10024";

            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString), "The connection string cannot be null. Please check your configuration.");
            }
        }

        /// <summary>
        /// Opens a new connection to the SQL Server database.
        /// </summary>
        /// <returns>
        /// An open <see cref="SqlConnection"/> object.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown if there is an error while attempting to open the connection.
        /// </exception>
        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new(connectionString);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while opening the database connection: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Closes and disposes of the provided SQL Server database connection.
        /// </summary>
        /// <param name="connection">
        /// The <see cref="SqlConnection"/> object to be closed and disposed.
        /// </param>
        /// <exception cref="Exception">
        /// Thrown if there is an error while attempting to close the connection.
        /// </exception>
        public void CloseConnection(SqlConnection connection)
        {
            try
            {
                connection.Close();
                connection.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while closing the database connection: " + ex.Message, ex);
            }
        }
    }
}
