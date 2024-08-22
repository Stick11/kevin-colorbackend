using System;
using System.Data;
using System.Data.SqlClient;
using ColorBackend.Model;

namespace ColorBackend.DataAccess
{
    /// <summary>
    /// The <c>ColorDA</c> class provides data access methods for managing colors in the database.
    /// It includes operations for creating, updating, deleting (logically), and retrieving colors.
    /// </summary>
    public class ColorDA
    {
        // SqlCommand object used for executing stored procedures
        private readonly SqlCommand cmdTabla = new();

        // Connection object to manage database connections
        private readonly Connection objConnection = new();

        /// <summary>
        /// Inserts a new color record in the database.
        /// </summary>
        /// <param name="p_Color">The <see cref="Color"/> object to be created.</param>
        /// <returns>The created <see cref="Color"/> object with the assigned ID.</returns>
        /// <exception cref="SqlException">Thrown when an error occurs during SQL execution.</exception>
        public Color Create(Color p_Color)
        {
            try
            {
                cmdTabla.CommandType = CommandType.StoredProcedure;
                cmdTabla.CommandText = "CreateColor";
                cmdTabla.Connection = objConnection.GetConnection();

                cmdTabla.Parameters.AddWithValue("@Name", p_Color.Name);

                var idParam = new SqlParameter("@ID", SqlDbType.Int);
                idParam.Direction = ParameterDirection.Output;
                cmdTabla.Parameters.Add(idParam);

                cmdTabla.ExecuteNonQuery();
                p_Color.ID = (int)idParam.Value;
                return p_Color;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while creating color: " + ex.Message);
                throw;
            }
            finally
            {
                cmdTabla.Parameters.Clear();
                objConnection.CloseConnection(cmdTabla.Connection);
            }
        }

        /// <summary>
        /// Updates an existing color record in the database.
        /// </summary>
        /// <param name="p_Color">The <see cref="Color"/> object with updated values.</param>
        /// <exception cref="SqlException">Thrown when an error occurs during SQL execution.</exception>
        public void Update(Color p_Color)
        {
            try
            {
                cmdTabla.CommandType = CommandType.StoredProcedure;
                cmdTabla.CommandText = "UpdateColor";
                cmdTabla.Connection = objConnection.GetConnection();

                cmdTabla.Parameters.AddWithValue("@ID", p_Color.ID);
                cmdTabla.Parameters.AddWithValue("@Name", p_Color.Name);

                cmdTabla.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while updating color: " + ex.Message);
                throw;
            }
            finally
            {
                cmdTabla.Parameters.Clear();
                objConnection.CloseConnection(cmdTabla.Connection);
            }
        }

        /// <summary>
        /// Logically deletes a color record from the database.
        /// </summary>
        /// <param name="id">The ID of the color to be deleted.</param>
        /// <exception cref="SqlException">Thrown when an error occurs during SQL execution.</exception>
        public void Delete(int id)
        {
            try
            {
                cmdTabla.CommandType = CommandType.StoredProcedure;
                cmdTabla.CommandText = "DeleteColor";
                cmdTabla.Connection = objConnection.GetConnection();

                cmdTabla.Parameters.AddWithValue("@ID", id);
                cmdTabla.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while deleting color: " + ex.Message);
                throw;
            }
            finally
            {
                cmdTabla.Parameters.Clear();
                objConnection.CloseConnection(cmdTabla.Connection);
            }
        }

        /// <summary>
        /// Retrieves a color record from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the color to be retrieved.</param>
        /// <returns>The <see cref="Color"/> object if found, or null if not found.</returns>
        /// <exception cref="SqlException">Thrown when an error occurs during SQL execution.</exception>
        public Color? GetByID(int id)
        {
            try
            {
                cmdTabla.CommandType = CommandType.StoredProcedure;
                cmdTabla.CommandText = "GetColorById";
                cmdTabla.Connection = objConnection.GetConnection();

                cmdTabla.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = cmdTabla.ExecuteReader();

                if (reader.Read())
                {
                    return new Color
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Name = Convert.ToString(reader["Name"])
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while retrieving color by ID: " + ex.Message);
                throw;
            }
            finally
            {
                cmdTabla.Parameters.Clear();
                objConnection.CloseConnection(cmdTabla.Connection);
            }
        }

        /// <summary>
        /// Retrieves all color records that are not logically deleted from the database.
        /// </summary>
        /// <returns>A list of <see cref="Color"/> objects.</returns>
        /// <exception cref="SqlException">Thrown when an error occurs during SQL execution.</exception>
        public List<Color> GetAll()
        {
            try
            {
                cmdTabla.CommandType = CommandType.StoredProcedure;
                cmdTabla.CommandText = "GetAllColors";
                cmdTabla.Connection = objConnection.GetConnection();

                SqlDataReader reader = cmdTabla.ExecuteReader();
                List<Color> lista = new();

                while (reader.Read())
                {
                    lista.Add(new Color
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Name = Convert.ToString(reader["Name"])
                    });
                }

                return lista;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL error while retrieving all colors: " + ex.Message);
                throw;
            }
            finally
            {
                cmdTabla.Parameters.Clear();
                objConnection.CloseConnection(cmdTabla.Connection);
            }
        }
    }
}
