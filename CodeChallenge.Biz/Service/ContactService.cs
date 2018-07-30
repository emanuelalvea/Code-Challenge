using CodeChallenge.Biz.Contract.Interface;
using CodeChallenge.Dal.Contract.Support;
using CodeChallenge.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Biz.Service
{
    public class ContactService : IContactService
    {
        private IUnitOfWork _unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Delete(int id)
        {
            this._unitOfWork.Repository<Contact>().Delete(id);
            this._unitOfWork.SaveChanges();
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return this._unitOfWork.Repository<Contact>().GetAll();
        }

        public Contact GetContact(int id)
        {
            return this._unitOfWork.Repository<Contact>().GetByID(id);
        }

        public async Task<Contact> GetContactAsync(int id)
        {
            return await this._unitOfWork.Repository<Contact>().GetByIdAsync(id);
        }

        public void SaveOrUpdate(Contact contact)
        {
            if (contact.Id == 0)
            {
                this._unitOfWork.Repository<Contact>().Insert(contact);
            }
            else
            {
                this._unitOfWork.Repository<Contact>().Update(contact);
            }

            this._unitOfWork.SaveChanges();
        }
    }
}
