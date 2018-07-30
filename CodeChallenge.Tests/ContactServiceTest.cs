using CodeChallenge.Biz.Contract.Interface;
using CodeChallenge.Biz.Service;
using CodeChallenge.Dal.Support;
using CodeChallenge.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace CodeChallenge.Tests
{
    [TestClass]
    public class ContactServiceTests
    {

        public ContactServiceTests()
        {

        }

        [TestMethod]
        public void GetAllContactEmptyList()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase();

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                var unitOfWorkMock = new Mock<UnitOfWork>();
                unitOfWorkMock.Object.Context = db;

                var service = new ContactService(unitOfWorkMock.Object);

                var result = service.GetAllContacts();

                Assert.AreEqual(0, result.Count());
            }
        }

        [TestMethod]
        public void GetAllContactWith3Results()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase();

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                db.Set<Contact>().Add(new Contact()
                {
                    Id = 1,
                    Name = "Contact 1"
                });
                db.Set<Contact>().Add(new Contact()
                {
                    Id = 2,
                    Name = "Contact 2"
                });
                db.Set<Contact>().Add(new Contact()
                {
                    Id = 3,
                    Name = "Contact 3"
                });
                db.SaveChangesAsync();
            }

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                var unitOfWorkMock = new Mock<UnitOfWork>();
                unitOfWorkMock.Object.Context = db;

                var service = new ContactService(unitOfWorkMock.Object);

                var result = service.GetAllContacts();

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Count());
            }
        }

        [TestMethod]
        public void GetContactById()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase();

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                db.Set<Contact>().Add(new Contact()
                {
                    Id = 1,
                    Name = "Contact 1"
                });
                db.SaveChangesAsync();

            }

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                var unitOfWorkMock = new Mock<UnitOfWork>();
                unitOfWorkMock.Object.Context = db;

                var service = new ContactService(unitOfWorkMock.Object);

                var result = service.GetContact(1);

                Assert.IsNotNull(result);
                Assert.AreEqual("Contact 1", result.Name);
            }
        }

        [TestMethod]
        public void InsertContact()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase();

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                var unitOfWorkMock = new Mock<UnitOfWork>();
                unitOfWorkMock.Object.Context = db;

                var service = new ContactService(unitOfWorkMock.Object);

                Contact contact = new Contact
                {
                    Name = "Contact mock",
                    Address = "Street 123",
                    Birtdate = new DateTime(),
                    Company = "Solstice",
                    Email = "mail@solstice.com",
                    PersonalPhoneNumber = "123456789",
                    ProfileImage = "/anImage.png",
                    WorkPhoneNumber = "123456789"
                };

                service.SaveOrUpdate(contact);

                Assert.AreEqual(1, db.Contact.Count());
            }
        }

        [TestMethod]
        public void UpdateContact()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase();

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                db.Set<Contact>().Add(new Contact()
                {
                    Id = 1,
                    Name = "Contact 1",
                    Address = "Street 123",
                    Birtdate = new DateTime(),
                    Company = "Solstice",
                    Email = "mail@solstice.com",
                    PersonalPhoneNumber = "123456789",
                    ProfileImage = "/anImage.png",
                    WorkPhoneNumber = "123456789"
                });
                db.SaveChangesAsync();

            }

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                var unitOfWorkMock = new Mock<UnitOfWork>();
                unitOfWorkMock.Object.Context = db;

                var service = new ContactService(unitOfWorkMock.Object);

                Contact contact = new Contact
                {
                    Id=1,
                    Name = "Updated name",
                    Address = "Street 123",
                    Birtdate = new DateTime(),
                    Company = "Solstice",
                    Email = "mail@solstice.com",
                    PersonalPhoneNumber = "123456789",
                    ProfileImage = "/anImage.png",
                    WorkPhoneNumber = "123456789"
                };

                service.SaveOrUpdate(contact);

                Assert.AreEqual(1, db.Contact.Count());
                Assert.AreEqual("Updated name", db.Contact.FirstOrDefault().Name);
            }
        }

        [TestMethod]
        public void DeleteContact()
        {
            var dbOptionsBuilder = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase();

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                db.Set<Contact>().Add(new Contact()
                {
                    Id = 1,
                    Name = "Contact 1",
                    Address = "Street 123",
                    Birtdate = new DateTime(),
                    Company = "Solstice",
                    Email = "mail@solstice.com",
                    PersonalPhoneNumber = "123456789",
                    ProfileImage = "/anImage.png",
                    WorkPhoneNumber = "123456789"
                });
                db.SaveChangesAsync();

            }

            using (var db = new Context(dbOptionsBuilder.Options))
            {
                var unitOfWorkMock = new Mock<UnitOfWork>();
                unitOfWorkMock.Object.Context = db;

                var service = new ContactService(unitOfWorkMock.Object);

                service.Delete(1);

                Assert.AreEqual(0, db.Contact.Count());
            }
        }
    }
}
