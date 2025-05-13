using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingAndPayments.Repository.Models;

namespace BillingAndPayments.BusinessLogic.Interfaces
{
    public interface IUser
    {
        void createUser(UserManagement userData);
        bool authenticateUser(string username, string password);
        UserManagement getUserById(int userId);
        void changeUserPassword(string userId, string oldPassword, string newPassword);
        UserManagement getUserByRole(string role);
        bool deactivateUserAccount(int userId);
    }
}
