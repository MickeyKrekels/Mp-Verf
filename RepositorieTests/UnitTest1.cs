using System;
using Common.ExtensionMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repositorie.Entities.Users;
using Repositorie.UnitOfWorks;

namespace RepositorieTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            UnitOfWorkRepository UnitOfWork = new UnitOfWorkRepository();

            var result = UnitOfWork.CustomerRepository.Get();
         }
        [TestMethod]
        public void CreateCostumer()
        {
            UnitOfWorkRepository UnitOfWork = new UnitOfWorkRepository();
            Customer costumer = new Customer { Id = Guid.NewGuid(), Name = "mickey", EmailAddress = "Test", Password = "ww" };
            UnitOfWork.CustomerRepository.Add(costumer);
        }
        [TestMethod]
        public void CreateAdmin()
        {
            UnitOfWorkRepository UnitOfWork = new UnitOfWorkRepository();
            var hash = SecurePasswordHasher.Hash("Pieter123");
            Admin admin = new Admin { Id = Guid.NewGuid(), Name = "Pieter", EmailAddress = "mp-krekels@hetnet.nl", Password = hash };
            UnitOfWork.AdminRepository.Add(admin);
        }
    }
}
