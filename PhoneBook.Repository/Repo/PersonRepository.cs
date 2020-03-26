using PhoneBook.Repository.Interfaces;
using PhoneBook.Repository.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Repository.Repo
{
    public class PersonRepository : IPersonRepository
    {
        public List<Person> All()
        {
            throw new NotImplementedException();
        }
    }
}

//        public List<Person> GetPeople()
//        {
//            List<Person> list = new List<Person>();
//            try
//            {
//                _connection.Open();
//                using (SqlCommand sqlcommand = new SqlCommand())
//                {
//                    sqlcommand.CommandText = "SELECT [Id], [Name], [Manufacturer], [Price], [Amount], [WithPrescription] FROM [Medicines]";
//                    sqlcommand.Connection = _connection;

//                    SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
//                    while (sqlDataReader.Read())
//                    {
//                        Medicine medicine = new Medicine()
//                        {
//                            Id = (int)sqlDataReader["Id"],
//                            Name = sqlDataReader["Name"].ToString(),
//                            Manufacturer = sqlDataReader["Manufacturer"].ToString(),
//                            Price = (decimal)sqlDataReader["Price"],
//                            Amount = (decimal)sqlDataReader["Amount"],
//                            WithPrescription = (bool)sqlDataReader["WithPrescription"]
//                        };

//                        list.Add(medicine);
//                        Console.WriteLine($"{medicine.Id.ToString().PadRight(5)}  {medicine.Name.PadRight(20)}  {medicine.Manufacturer.PadRight(20)}  {medicine.Price.ToString().PadRight(20)}  {medicine.Amount.ToString().PadRight(20)}  {medicine.WithPrescription.ToString().PadRight(20)}");
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
//    }
//}
