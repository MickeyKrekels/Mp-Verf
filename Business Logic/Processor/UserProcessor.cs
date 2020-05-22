using Business_Logic.Models;
using Common.ExtensionMethods;
using Repositorie.Base.Repositories;
using Repositorie.Entities.Users;
using Repositorie.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static UserModel UserLogin(UserModel userModel)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            List<User> totalAcounts = new List<User>();
            totalAcounts.AddRange(unitOfWork.CustomerRepository.Get());
            totalAcounts.AddRange(unitOfWork.AdminRepository.Get());

            var Result = totalAcounts
                .Where(x => SecurePasswordHasher.Verify(userModel.Password, x.Password) == true &&
                x.EmailAddress == userModel.Email)
                .SingleOrDefault();

            if (Result == null)
                return null;

            var model = ConvertToModel(Result);

            return model;
        }

        public static void UpdateShoppingCart(Guid id, List<StoreItemModel> storeItemModels)
        {
            UnitOfWorkRepository unitOfWork = new UnitOfWorkRepository();

            var StoreItems = StoreItemProcessor.ConvertModelToStoreItem(storeItemModels);

            if (StoreItems == null)
                return;

            unitOfWork.CustomerRepository.UpdateShoppingCart(id, StoreItems);
        }

        public static UserModel ConvertToModel(User user)
        {
            if (user == null)
                return null;

            List<StoreItemModel> shoppingCart = new List<StoreItemModel>();

            if (user is Customer)
            {
                Customer customer = (Customer)user;

                var userShoppingCart = StoreItemProcessor.ConvertStoreItemToModel(customer.ShoppingCart);

                if (userShoppingCart != null)
                {
                    shoppingCart = userShoppingCart;
                }
            }

            UserModel model = new UserModel
            {
                Id = user.Id,
                Email = user.EmailAddress,
                Name = user.Name,
                Password = user.Password,
                ShoppingCart = shoppingCart
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

        public static List<UserModel> ConvertAllUsersToModel()
        {
            var users = GetUsers();
            var models = ConvertToModel(users);

            return models;

        }

        public static UserModel ConvertUserToModel(Guid id)
        {
            var user = GetUser(id);
            var model = ConvertToModel(user);

            return model;
        }

        public static string ReturnRole(Guid id)
        {
            var user = GetUser(id);

            if (user is Customer)
            {
                return typeof(Customer).Name;
            }
            if (user is Admin)
            {
                return typeof(Admin).Name;
            }
            return "";
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
    }
}
