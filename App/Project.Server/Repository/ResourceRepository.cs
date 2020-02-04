using System;
using System.Data.Entity;
using System.Linq;
using Project.Repository;
using Project.Security.Models;


namespace Project.Server.Repository
{
    public interface IResourceRepository : IBaseRepository<SecurityModels.Resource>
    {
        SecurityModels.Resource CheckResource(string route);
    }




    public class ResourceRepository: BaseRepository<SecurityModels.Resource>, IResourceRepository
    {
        private readonly ApplicationDbContext _db;

        public ResourceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public SecurityModels.Resource CheckResource(string route)
        {
            return _db.Resources.FirstOrDefault(x => x.Route.ToLower() == route.ToLower());
        }

    }
}