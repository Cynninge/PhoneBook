using PhoneBook.Repository.Interfaces;
using PhoneBook.Repository.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Repository.StubRepo
{
    public class StubPersonRepository : IPersonRepository
    {
        

        public List<Person> All()
        {
            List<Person> people = new List<Person>()
            {
                new Person() { Id = 555, FirstName = "dsdfsdf"}
            };

            return people;
        }

        public Person GetbyId(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Person person)
        {
            throw new NotImplementedException();
        }

        int IPersonRepository.Add(Person person)
        {
            throw new NotImplementedException();
        }

        Person IPersonRepository.SearchByLastname(string lastname)
        {
            throw new NotImplementedException();
        }
    }
}
