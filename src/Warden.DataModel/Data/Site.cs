using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Warden.DataModel.Entities;

namespace Warden.DataModel
{
    public class Site : IEntity
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(255)]
        [MinLength(1)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// TODO: change to an object
        /// </summary>
        [MaxLength(255)]
        [MinLength(1)]
        [Required]
        public string Address { get; set; }
    }
}
