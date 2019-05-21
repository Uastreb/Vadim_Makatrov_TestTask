using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vadim_Makatrov_TestTask
{
    class DB_connection
    {
        SqlConnectionStringBuilder connect = new SqlConnectionStringBuilder();
        SqlConnection cn = new SqlConnection();

        public void ReadAuthors()
        {
            try
            {
                cn.Open();
                string strSQL = "SELECT * FROM Authors";
                SqlCommand myCommand = new SqlCommand(strSQL, cn);
                SqlDataReader dr = myCommand.ExecuteReader();
                while (dr.Read())
                    Console.WriteLine("ID: {0} Фамилия: {1}", dr[0], dr[1]);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public int GetIdBook(string Name, int Year)
        {
            cn.Open();
            SqlCommand myCommand = new SqlCommand(string.Format("SELECT id FROM Books Where Name = '{0}' and Year = '{1}'", Name, Year), cn);
            SqlDataReader dr = myCommand.ExecuteReader();
            int val = 0;
            while (dr.Read())
                val = Convert.ToInt32(dr[0]);
            cn.Close();
            return val;
        }

        public int GetIdAuthor(string Surname)
        {
            cn.Open();
            SqlCommand myCommand = new SqlCommand(string.Format("SELECT id FROM Authors Where Surname = '{0}'", Surname), cn);
            SqlDataReader dr = myCommand.ExecuteReader();
            int val = 0;
            while (dr.Read())
                val = Convert.ToInt32(dr[0]);
            cn.Close();
            return val;
        }

        public void AddLink(int id_Author, int id_Book)
        {
            try
            {
                cn.Open();
                string sql = string.Format("Insert Into Authors_Books(id_Author, id_Book) Values(@id_Author, @id_Book)");

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id_Author", id_Author);
                    cmd.Parameters.AddWithValue("@id_Book", id_Book);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public void ReadBooks()
        {
            try
            {
                cn.Open();
                string strSQL = "SELECT * FROM Books";
                SqlCommand myCommand = new SqlCommand(strSQL, cn);
                SqlDataReader dr = myCommand.ExecuteReader();
                while (dr.Read())
                    Console.WriteLine("ID: {0} Название: {1} Год: {2}", dr[0], dr[1], dr[2]);
            }
            catch (SqlException ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public void AddBook(string Name, int Year)
        {
            try
            {
                cn.Open();
                string sql = string.Format("Insert Into Books" +
                   "(Name, Year) Values(@Name, @Year)");

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Name", Name);
                    cmd.Parameters.AddWithValue("@Year", Year);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public void AddAuthor(string Surname)
        {
            try
            {
                cn.Open();
                string sql = string.Format("Insert Into Authors (Surname) Values(@Surname)");

                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@Surname", Surname);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

        public DB_connection()
        {
            connect.InitialCatalog = "Books_DB";
            connect.DataSource = @"(local)\SQLEXPRESS";
            connect.ConnectTimeout = 30;
            connect.IntegratedSecurity = true;

            cn.ConnectionString = connect.ConnectionString;
        }

        public void GetBooksAuthors(int id_Author)
        {
            cn.Open();
            SqlCommand myCommand = new SqlCommand(string.Format("SELECT Books.id, Books.Name FROM Books, Authors_Books Where Books.id = Authors_Books.id_Book and Authors_Books.id_Author = '{0}'", id_Author), cn);
            SqlDataReader dr = myCommand.ExecuteReader();
            while (dr.Read())
                Console.WriteLine("ID: {0} Название: {1}", dr[0], dr[1]);
            cn.Close();
        }

        public void DeleteBook(int id_Book)
        {
            try
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteBook", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@id";
                    param.SqlDbType = SqlDbType.Int;
                    param.Value = id_Book;
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
