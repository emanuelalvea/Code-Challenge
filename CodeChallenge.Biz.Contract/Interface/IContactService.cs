using CodeChallenge.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Biz.Contract.Interface
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAllContacts();

        Contact GetContact(int id);

        Task<Contact> GetContactAsync(int id);

        void SaveOrUpdate(Contact contact);

        void Delete(int id);

    }
}
