using System;
using System.Collections.Generic;
using System.Linq;
using Project.Security.Models;
using Project.Server.Repository;
using Project.Service;


namespace Project.Server.Service
{
    public interface IResourceService :IBaseService<SecurityModels.Resource>
    {
    }


    public class ResourceService: BaseService<SecurityModels.Resource>, IResourceService
    {
        private readonly IResourceRepository _repository;

        public ResourceService(IResourceRepository repository) : base(repository)
        {
            _repository = repository;
        }
     

    }
}