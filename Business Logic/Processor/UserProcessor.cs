using Business_Logic.Models;
using Common.ExtensionMethods;
using Repositorie.Base.Repositories;
using Repositorie.Entities.Users;
using Repositorie.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic.Processor
{
    public static class UserProcessor
    {
        public static void CreateCustomer(UserModel model)
        {
            if (model == null)
                return;
            
            string HashedPassowrd = SecurePasswordHasher.Hash(model.Password);
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            Customer customer = new Customer
            {
                Id = Guid.NewGuid(),
                Name = model.Name,
                EmailAddress = model.Email,
                Password = HashedPassowrd,               
            };

            unitOfWork.CustomerRepository.Add(customer);
        } 

        public static User UserLogin(UserModel model)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            List<User> totalAcounts = new List<User>();
            totalAcounts.AddRange(unitOfWork.CustomerRepository.Get());
            totalAcounts.AddRange(unitOfWork.AdminRepository.Get());

            var Result = totalAcounts
                .Where(x => SecurePasswordHasher.Verify(model.Password, x.Password) == true &&
                x.EmailAddress == model.Email)
                .SingleOrDefault();

            if (Result == null)
                return null;


            return Result;
        }

        public static UserModel ConvertToModel(User user)
        {
            if(user == null)
                return null;

            UserModel model = new UserModel
            {
                Id = user.Id,
                Email = user.EmailAddress,
                Name = user.Name,
                Password = user.Password
            };
            return model;
        }

        public static List<UserModel> ConvertToModel(List<User> users)
        {
            if (users == null)
                return null;

            List<UserModel> models = new List<UserModel>();
            foreach (var user in users)
            {
                UserModel model = new UserModel
                {
                    Id = user.Id,
                    Email = user.EmailAddress,
                    Name = user.Name,
                    Password = user.Password
                };
                models.Add(model);
            }
            return models;
        }

        public static List<User> GetUsers()
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            List<User> totalAcounts = new List<User>();
            totalAcounts.AddRange(unitOfWork.CustomerRepository.Get());
            totalAcounts.AddRange(unitOfWork.AdminRepository.Get());

            return totalAcounts;
        }

        public static User GetUser(Guid id)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            List<User> totalAcounts = new List<User>();
            totalAcounts.AddRange(unitOfWork.CustomerRepository.Get());
            totalAcounts.AddRange(unitOfWork.AdminRepository.Get());

            var result = totalAcounts.Where(x => x.Id == id).SingleOrDefault();

            return result;
        }

        public static string ReturnRole(User user)
        {
            if(user is Customer)
            {
                return typeof(Customer).Name;
            }
            if (user is Admin)
            {
                return typeof(Admin).Name;
            }
            return "";
        }

    }
}
