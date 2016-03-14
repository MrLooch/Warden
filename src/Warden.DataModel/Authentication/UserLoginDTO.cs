using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Warden.DataModel.Entities;

namespace Warden.DataModel.Authentication
{
    public class UserLoginDTO : EntityBase
    {
        public UserLoginDTO()
        {
            RememberMe = false;
        }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
