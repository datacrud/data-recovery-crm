﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Project.Repository;
using Project.Security.Models;


namespace Project.Server.Repository
{
    public interface IPermissionRepository : IBaseRepository<SecurityModels.Permission>
    {               
        SecurityModels.Permission CheckPermission(RequestModels.PermissionRequestModel model);
        IQueryable<SecurityModels.Permission> GetListById(string request);
        bool AddList(List<SecurityModels.Permission> models);
    }



    public class PermissionRepository : BaseRepository<SecurityModels.Permission>, IPermissionRepository
    {
        private readonly ApplicationDbContext _db;

        public PermissionRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }



        public IQueryable<SecurityModels.Permission> GetListById(string request)
        {
            var permissions = _db.Permissions.Where(x => x.RoleId == request).AsQueryable();
            return permissions;
        }

        public SecurityModels.Permission CheckPermission(RequestModels.PermissionRequestModel model)
        {

            return _db.Permissions.FirstOrDefault(x => x.RoleId == model.RoleId && x.ResourceId == model.ResourceId);
        }        
        

        public bool AddList(List<SecurityModels.Permission> models)
        {
            try
            {
                var permissions = models.Select(model => _db.Permissions.Where(x => x.RoleId == model.RoleId).ToList()).FirstOrDefault();

                if (permissions != null)
                {
                    _db.Permissions.RemoveRange(permissions);
                }

                if (models.Any(model => model.ResourceId != "00000000-0000-0000-0000-000000000000"))
                {
                    _db.Permissions.AddRange(models);
                }                

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