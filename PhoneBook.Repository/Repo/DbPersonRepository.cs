using PhoneBook.Repository.Interfaces;
using PhoneBook.Repository.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace PhoneBook.Repository.Repo
{
    public class DbPersonRepository : IPersonRepository, IDisposable
    {
        System.Data.SqlClient.SqlConnection _connection; public DbPersonRepository()
        {
            string connectionString = "Integrated Security=SSPI;" + "Data Source=.\\SQLEXPRESS;" + "Initial Catalog=PhoneBook;";
            _connection = new System.Data.SqlClient.SqlConnection();
            _connection.ConnectionString = connectionString;
        }
      
        public List<Person> All()
        {
            List<Person> list = new List<Person>();
            
            try
            {
                _connection.Open();
                using (System.Data.SqlClient.SqlCommand sqlcommand = new System.Data.SqlClient.SqlCommand())
                {
                    sqlcommand.CommandText = "SELECT [Id],[FirstName],[LastName],[Phone],[Email],[CreateStamp],[ModifStamp] " +
                        "FROM [Person] ";
                    sqlcommand.Connection = _connection;
                    System.Data.SqlClient.SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Person person = new Person();
                        person.Id = (int)sqlDataReader["Id"];
                        person.FirstName = sqlDataReader["FirstName"].ToString();
                        person.LastName = sqlDataReader["LastName"].ToString();
                        person.PhoneNumber = sqlDataReader["Phone"].ToString();
                        person.Email = sqlDataReader["Email"].ToString();
                        object createStamp = sqlDataReader["CreateStamp"];
                        if (createStamp != null && !(createStamp is System.DBNull))
                        {
                            person.CreateStamp = (DateTime?)sqlDataReader["CreateStamp"];
                        }
                        object modiffStamp = sqlDataReader["ModifStamp"];

                        if (modiffStamp != null && !(modiffStamp is System.DBNull))
                        {
                            person.ModifStamp = (DateTime?)sqlDataReader["ModifStamp"];
                        }
                        list.Add(person);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return list;
        }

        public Person GetbyId(int id)
        {
            try
            {
                _connection.Open();
                using (System.Data.SqlClient.SqlCommand sqlcommand = new System.Data.SqlClient.SqlCommand())
                {
                    sqlcommand.CommandText = "SELECT [Id],[FirstName],[LastName],[Phone],[Email],[CreateStamp],[ModifStamp] " +
                        "FROM [Person] WHERE ID = " + id;
                    sqlcommand.Connection = _connection; System.Data.SqlClient.SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();

                    if (sqlDataReader.Read())
                    {
                        Person person = new Person();
                        person.Id = (int)sqlDataReader["Id"];
                        person.FirstName = sqlDataReader["FirstName"].ToString();
                        person.LastName = sqlDataReader["LastName"].ToString();
                        person.PhoneNumber = sqlDataReader["Phone"].ToString();
                        person.Email = sqlDataReader["Email"].ToString();
                        object createStamp = sqlDataReader["CreateStamp"];
                        if (createStamp != null && !(createStamp is System.DBNull))
                        {
                            person.CreateStamp = (DateTime?)sqlDataReader["CreateStamp"];
                        }
                        object modiffStamp = sqlDataReader["ModifStamp"];

                        if (modiffStamp != null && !(modiffStamp is System.DBNull))
                        {
                            person.ModifStamp = (DateTime?)sqlDataReader["ModifStamp"];
                        }
                        return person;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return null;
        }

        public void Save(Person person)
        {
            try
            {
                _connection.Open();
                using (System.Data.SqlClient.SqlCommand sqlcommand = new System.Data.SqlClient.SqlCommand())
                {
                    sqlcommand.CommandText = @"UPDATE [Person] 
                               SET [FirstName] = '" + person.FirstName + @"'
                                  ,[LastName] = '" + person.LastName + @"'
                                  ,[Phone] = '" + person.PhoneNumber + @"'
                                  ,[Email] =  '" + person.Email + @"'
                                  ,[ModifStamp] = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"'
                             WHERE ID = " + person.Id;
                    sqlcommand.Connection = _connection; int count = sqlcommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }
        public void Remove(int id)
        {
            try
            {
                _connection.Open();
                using (System.Data.SqlClient.SqlCommand sqlcommand = new System.Data.SqlClient.SqlCommand())
                {
                    sqlcommand.CommandText = @" DELETE FROM [Person] WHERE ID = " + id;
                    sqlcommand.Connection = _connection;
                    int count = sqlcommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public int Add(Person person)
        {
            try
            {
                _connection.Open();
                using (System.Data.SqlClient.SqlCommand sqlcommand = new System.Data.SqlClient.SqlCommand())
                {
                    sqlcommand.CommandText = @"
                               INSERT INTO [dbo].[Person] ([FirstName] ,[LastName] ,[Phone] ,[Email] ,[CreateStamp] ,[ModifStamp])
                               OUTPUT INSERTED.ID
                                VALUES (
                                '" + person.FirstName + @"',
                                '" + person.LastName + @"',
                                '" + person.PhoneNumber + @"',
                                '" + person.Email + @"',
                                '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"',
                                '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"');";
                    sqlcommand.Connection = _connection; int count = (int)sqlcommand.ExecuteScalar(); return count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return -1;            
        }

        public Person SearchByLastname(string lastname)
        {
            try
            {
                _connection.Open();
                using (System.Data.SqlClient.SqlCommand sqlcommand = new System.Data.SqlClient.SqlCommand())
                {
                    sqlcommand.CommandText = "SELECT [Id],[FirstName],[LastName],[Phone],[Email],[CreateStamp],[ModifStamp] " +
                        "FROM [Person] WHERE LastName = " + lastname;
                    sqlcommand.Connection = _connection; System.Data.SqlClient.SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
                    if (sqlDataReader.Read())
                    {
                        Person person = new Person();
                        person.Id = (int)sqlDataReader["Id"];
                        person.FirstName = sqlDataReader["FirstName"].ToString();
                        person.LastName = sqlDataReader["LastName"].ToString();
                        person.PhoneNumber = sqlDataReader["Phone"].ToString();
                        person.Email = sqlDataReader["Email"].ToString();
                        object createStamp = sqlDataReader["CreateStamp"];
                        if (createStamp != null && !(createStamp is System.DBNull))
                        {
                            person.CreateStamp = (DateTime?)sqlDataReader["CreateStamp"];
                        }
                        object modiffStamp = sqlDataReader["ModifStamp"];
                        if (modiffStamp != null && !(modiffStamp is System.DBNull))
                        {
                            person.ModifStamp = (DateTime?)sqlDataReader["ModifStamp"];
                        }
                        return person;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return null;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }       
    }
}


//    public class DbPersonRepository : IPersonRepository, IDisposable
//    {
//        System.Data.SqlClient.SqlConnection _connection;
//        public DbPersonRepository()
//        {
//            string connectionString = "Integrated Security=SSPI;" + "Data Source=.\\SQLEXPRESS;" + "Initial Catalog=PhoneBook;";
//            _connection = new System.Data.SqlClient.SqlConnection();
//            _connection.ConnectionString = connectionString;
//        }

//        public List<Person> All()
//        {
//            List<Person> list = new List<Person>(); try
//            {
//                _connection.Open();
//                using (System.Data.SqlClient.SqlCommand sqlcommand = new System.Data.SqlClient.SqlCommand())
//                {
//                    sqlcommand.CommandText = "SELECT [Id],[FirstName],[LastName],[Phone],[Email],[CreateStamp],[ModifStamp] " +
//                        "FROM [Person] ";
//                    sqlcommand.Connection = _connection; System.Data.SqlClient.SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
//                    while (sqlDataReader.Read())
//                    {
//                        Person person = new Person(); person.Id = (int)sqlDataReader["Id"];
//                        person.FirstName = sqlDataReader["FirstName"].ToString();
//                        person.LastName = sqlDataReader["LastName"].ToString();
//                        person.PhoneNumber = sqlDataReader["Phone"].ToString();
//                        person.Email = sqlDataReader["Email"].ToString();
//                        object createStamp = sqlDataReader["CreateStamp"];
//                        if (createStamp != null && !(createStamp is System.DBNull))
//                        {
//                            person.CreateStamp = (DateTime?)sqlDataReader["CreateStamp"];
//                        }
//                        object modiffStamp = sqlDataReader["ModifStamp"];
//                        if (modiffStamp != null && !(modiffStamp is System.DBNull))
//                        {
//                            person.ModifStamp = (DateTime?)sqlDataReader["ModifStamp"];
//                        }
//                        list.Add(person);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//            finally
//            {
//                _connection.Close();
//            }
//            return list;

//        }
//        public void Dispose()
//        {
//            _connection.Dispose();
//        }

//        public Person GetbyId(int id)
//        {
//            try
//            {
//                List<Person> list = new List<Person>(); try
//                {
//                    _connection.Open();
//                    using (System.Data.SqlClient.SqlCommand sqlcommand = new System.Data.SqlClient.SqlCommand())
//                    {
//                        sqlcommand.CommandText = "SELECT [Id],[FirstName],[LastName],[Phone],[Email],[CreateStamp],[ModifStamp] " +
//                            "FROM [Person] ";
//                        sqlcommand.Connection = _connection; System.Data.SqlClient.SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
//                        while (sqlDataReader.Read())
//                        {
//                            Person person = new Person(); person.Id = (int)sqlDataReader["Id"];
//                            person.FirstName = sqlDataReader["FirstName"].ToString();
//                            person.LastName = sqlDataReader["LastName"].ToString();
//                            person.PhoneNumber = sqlDataReader["Phone"].ToString();
//                            person.Email = sqlDataReader["Email"].ToString();
//                            object createStamp = sqlDataReader["CreateStamp"];
//                            if (createStamp != null && !(createStamp is System.DBNull))
//                            {
//                                person.CreateStamp = (DateTime?)sqlDataReader["CreateStamp"];
//                            }
//                            object modiffStamp = sqlDataReader["ModifStamp"];
//                            if (modiffStamp != null && !(modiffStamp is System.DBNull))
//                            {
//                                person.ModifStamp = (DateTime?)sqlDataReader["ModifStamp"];
//                            }
//                            list.Add(person);
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine(ex.Message);
//                }
//                finally
//                {
//                    _connection.Close();
//                }
//                return list;
//            }
//            catch (Exception)
//            {

//                throw new NotImplementedException();
//            }

//        }

//        public void Save(Person person)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}