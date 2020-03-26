using PhoneBook.Repository.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Repository.Interfaces
{
    public interface IPersonRepository
    {
        List<Person> All();
        Person GetbyId(int id);
        void Save(Person person);
        void Remove(int id);
        int Add(Person person);
        Person SearchByLastname(string lastname);
    }
}
