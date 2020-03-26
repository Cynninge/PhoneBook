using PhoneBook.Repository.Interfaces;
using PhoneBook.Repository.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Web.Models
{
    public class IndexModel
    {
        public List<Person> People { get; set; }

        public int? SelectId { get; set; }

        public void Init(IPersonRepository personRepository)
        {
            People = personRepository.All();
        }


    }
}
