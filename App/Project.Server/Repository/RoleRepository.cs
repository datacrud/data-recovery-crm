using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using Project.Repository;
using Project.Security.Models;

namespace Project.Server.Repository
{
    public interface IRoleRepository : IBaseRepository<IdentityRole>
    {        
    }

    public class RoleRepository : BaseRepository<IdentityRole>, IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
    }
}