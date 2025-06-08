using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace databaseConnection
{
    internal class Program
    {
        static string connectionString;
        static SqlConnection conn;
        static SqlCommand sqlCommand;
        static SqlDataReader reader;

        static void Main(string[] args)
        {
            bool connectionStatus;
            connectionStatus = databaseConnection();

            if (connectionStatus)
            {
                Console.WriteLine("Connected to Database");
                rediredtor();
            }
            else
            {
                Console.WriteLine("Connection Failed");
            }
        }

        public static bool databaseConnection()
        {
            connectionString = @"Data Source=BLAK;Initial Catalog=haitomnsg;Integrated Security=True";

            conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                return true;
            }
            catch
            {
                Console.ReadKey();
            }
            return false;
        }

        public static void rediredtor()
        {
            string point;

            Console.Clear();
            Console.WriteLine("\n\n 1. Select Data \n 2. Insert Data \n 3. Delete Data \n 4. Update Data");
            Console.Write("\n\n\n Enter To go : ");
            point = Console.ReadLine();

            switch (point)
            {
                case "1":
                    readData();
                    break;
                case "2":
                    writeData();
                    break;
                case "3":
                    deleteData();
                    break;
                case "4":
                    updateDate();
                    break;
                default:
                    Console.WriteLine("Wrong Input");
                    break;
            }
        }

        public static  void readData()
        {
            string sqlQuery, output = "";
            sqlQuery = "select * from users";
            sqlCommand = new SqlCommand(sqlQuery, conn);
            reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                output = reader.GetValue(0) + " - " + reader.GetValue(1) + " - " + reader.GetValue(2);
                Console.WriteLine("\n "+output);
            }

            reader.Close();

            Console.ReadKey();
            rediredtor();
        }

        public static void writeData()
        {
            SqlDataAdapter sqlData = new SqlDataAdapter();

            string id, username, password;

            Console.Write("\n ID : ");
            id = Console.ReadLine();
            Console.Write("\n Username : ");
            username = Console.ReadLine();
            Console.Write("\n Password : ");
            password = Console.ReadLine();

            string sqlQuery = "insert into users (id, username, password) values ('"+id+"','"+username+"','"+password+"')";
            try
            {
                sqlCommand = new SqlCommand(sqlQuery, conn);
                sqlData.InsertCommand = new SqlCommand(sqlQuery, conn);
                sqlData.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Inserted");
            }
            catch
            {
                Console.WriteLine("Insert Failed");
            }

            Console.ReadKey();
            sqlCommand.Dispose();
            rediredtor();
        }

        public static void deleteData()
        {
            SqlDataAdapter sqlData = new SqlDataAdapter();

            string id, username, password;

            Console.Write("\n ID to Delete : ");
            id = Console.ReadLine();

            string sqlQuery = "delete users where id = '" + id + "'";
            try
            {
                sqlCommand = new SqlCommand(sqlQuery, conn);
                sqlData.InsertCommand = new SqlCommand(sqlQuery, conn);
                sqlData.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Deleted");
            }
            catch
            {
                Console.WriteLine("Delete Failed");
            }

            Console.ReadKey();
            sqlCommand.Dispose();
            rediredtor();
        }

        public static void updateDate()
        {
            SqlDataAdapter sqlData = new SqlDataAdapter();

            string id, username, password;

            Console.Write("\n ID to update : ");
            id = Console.ReadLine();

            Console.Write("\n New Username : ");
            username = Console.ReadLine();
            Console.Write("\n NewPassword : ");
            password = Console.ReadLine();

            string sqlQuery = "update users set username = '"+username+"', password = '"+password+"' where id = '" + id + "'";
            try
            {
                sqlCommand = new SqlCommand(sqlQuery, conn);
                sqlData.InsertCommand = new SqlCommand(sqlQuery, conn);
                sqlData.InsertCommand.ExecuteNonQuery();
                Console.WriteLine("Updated");
            }
            catch
            {
                Console.WriteLine("Update Failed");
            }

            Console.ReadKey();
            sqlCommand.Dispose();
            rediredtor();
        }
    }
}
