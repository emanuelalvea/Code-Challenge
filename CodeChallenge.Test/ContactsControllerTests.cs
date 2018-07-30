using CodeChallenge.Biz.Contract.Interface;
using CodeChallenge.Biz.Service;
using CodeChallenge.Controllers;
using CodeChallenge.Dal.Support;
using CodeChallenge.Entities.Models;
using System;
using System.Linq;
using Xunit;

namespace CodeChallenge.Test
{
 [CollectionDefinition("Contacts")]
    public class ContactsControllerTests : ICollectionFixture<Contact>
    {
        private ContactsController contactsController;

        public ContactsControllerTests()
        {
            this.contactsController = new ContactsController(new ContactService(new UnitOfWork()));
        }
        [Fact]
        public void GetAllContact()
        {
            var contacts = this.contactsController.GetContact();
            var count = contacts.ToList().Count;
            Assert.Equal(0,count);
        }
    }
}
