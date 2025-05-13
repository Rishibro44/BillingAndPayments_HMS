using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingAndPayments.Repository.Models

{
    public class UserManagement
    {
        [Key]
        public int userId { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        //public enum role
        //{
        //    PAID,
        //    UNPAID
        //}
        //public role _role { get; set; }


        public string status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime LastLogin { get; set; }





    }
}

