﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Project.Repository;
using Project.Security.Models;
using Project.Security.Startup;

namespace Project.Server.Repository
{
    public interface IProfileRepository : IBaseRepository<ApplicationUser>
    {
        Task<IdentityResult> UpdateProfile(ApplicationUser user);
        bool UpdatePassword(RequestModels.ChangePasswordRequestModel model);
    }



    public class ProfileRepository : BaseRepository<ApplicationUser>, IProfileRepository
    {
        private readonly ApplicationDbContext _db;

        public ProfileRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }


        //Profile Section
        public Task<IdentityResult> UpdateProfile(ApplicationUser user)
        {
            Task<IdentityResult> updateAsync;
            try
            {
                updateAsync = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().UpdateAsync(user);
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

            return updateAsync;
        }

        public bool UpdatePassword(RequestModels.ChangePasswordRequestModel model)
        {
            try
            {
                var id = HttpContext.Current.User.Identity.GetUserId();
                var user = _db.Users.Find(id);
                user.PasswordHash = new PasswordHasher().HashPassword(model.NewPassword);
                _db.Entry(user).State = EntityState.Modified;
                _db.SaveChanges();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return true;
        }


    }
}