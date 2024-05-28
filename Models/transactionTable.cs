using System;
using System.Data.SqlClient;

namespace CloudDevelopment.Models
{
    public class transactionTable
    {
        // Connection string for the database
        public static string con_string = "Server=tcp:stevenb.database.windows.net,1433;Initial Catalog=stevenb;Persist Security Info=False;User ID=Steven;Password=Eastarmy97;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        // Properties representing columns in the transactionTable
        public int TransactionID { get; set; }
        public string UserName { get; set; }
        public int ProductID { get; set; }

        // Parameterless constructor
        public transactionTable() { }

        // Parameterized constructor
        public transactionTable(string userName, int productID)
        {
            UserName = userName;
            ProductID = productID;
        }

        // Method to insert a new transaction record into the database
        public int InsertTransaction()
        {
            try
            {
                // SQL query to insert a new record into the transactionTable
                string sql = "INSERT INTO transactionTable (userName, productID) VALUES (@UserName, @ProductID)";

                // SqlCommand object to execute the SQL query
                using (SqlConnection con = new SqlConnection(con_string))
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@ProductID", ProductID);

                    // Open database connection
                    con.Open();

                    // Execute the SQL query and get the number of rows affected
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Return the number of rows affected
                    return rowsAffected;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // For now, rethrow the exception
                throw ex;
            }
        }

        // Static method to get all transactions (optional, for completeness)
        public static List<transactionTable> GetAllTransactions()
        {
            List<transactionTable> transactions = new List<transactionTable>();

            string sql = "SELECT * FROM transactionTable";

            using (SqlConnection con = new SqlConnection(con_string))
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transactionTable transaction = new transactionTable
                        {
                            TransactionID = Convert.ToInt32(reader["transactionID"]),
                            UserName = Convert.ToString(reader["userName"]),
                            ProductID = Convert.ToInt32(reader["productID"])
                        };
                        transactions.Add(transaction);
                    }

                    reader.Close();
                }
            }

            return transactions;
        }
    }
}
