﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Project.Security.Models
{
    public class SecurityModels
    {
        public abstract class IdentityBaseEntity
        {
            [Key]
            public string Id { get; set; }
        }

        public class IdentityEntity
        {
             
        }

        public class Resource : IdentityBaseEntity
        {            

            [Required]
            public string Name { get; set; }

            [Required]
            public string Route { get; set; }

            [Required]
            public bool IsPublic { get; set; }

        }


        public class Permission : IdentityBaseEntity
        {
            [Required]
            public string RoleId { get; set; }

            [Required]
            public string RoleName { get; set; }

            [Required]
            public string ResourceId { get; set; }

            [ForeignKey("RoleId")]
            public virtual IdentityRole AspNetRole { get; set; }

            [ForeignKey("ResourceId")]
            public virtual Resource Resource { get; set; }
        }

        
    }
}