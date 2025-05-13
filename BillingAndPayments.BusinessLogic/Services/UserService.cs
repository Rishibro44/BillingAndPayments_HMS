using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BillingAndPayments.BusinessLogic.Interfaces;
using BillingAndPayments.Repository.Models;

namespace BillingAndPayments.BusinessLogic.Services
{
    public class UserService : IUser
    {
        private readonly BillingContext _dbContext;

        public UserService(BillingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool authenticateUser(string username, string password)
        {
            // Retrieve the user by username
            var user = _dbContext.UserManagements.FirstOrDefault(u => u.userName == username);

            if (user == null)
                return false;

            // Verify the password
            return VerifyPassword(password, user.password);
        }

        public void changeUserPassword(string userId, string oldPassword, string newPassword)
        {
            // Retrieve the user by userId
            var user = _dbContext.UserManagements.FirstOrDefault(u => u.userId.ToString() == userId);

            if (user == null)
                throw new Exception("User not found.");

            // Verify the old password
            if (!VerifyPassword(oldPassword, user.password))
                throw new Exception("Old password is incorrect.");

            // Hash the new password
            user.password = HashPassword(newPassword);

            // Save changes to the database
            _dbContext.SaveChanges();
        }

        public void createUser(UserManagement userData)
        {
            if (userData != null)
            {
                userData.createdAt = DateTime.Now;
                userData.LastLogin = DateTime.Now;

                // Hash the password before saving
                userData.password = HashPassword(userData.password);

                _dbContext.UserManagements.Add(userData);
                _dbContext.SaveChanges();
            }
        }

        public bool deactivateUserAccount(int userId)
        {
            bool isDeactivated = false;
            var user = _dbContext.UserManagements.FirstOrDefault(u => u.userId == userId);
            if (user != null)
            {
                user.status = "Inactive";
                _dbContext.SaveChanges();
                isDeactivated = true;
            }
            return isDeactivated;
        }

        public UserManagement getUserById(int userId)
        {
            return _dbContext.UserManagements.FirstOrDefault(user => user.userId == userId);
        }

        public UserManagement getUserByRole(string role)
        {
            return _dbContext.UserManagements.FirstOrDefault(user => user.status == role);
        }

        // Helper method to hash a password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        // Helper method to verify a password
        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            var inputPasswordHash = HashPassword(inputPassword);
            return inputPasswordHash == storedPasswordHash;
        }
    }
}
