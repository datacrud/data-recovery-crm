using System;
using System.Collections.Generic;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Project.Model;
using Project.RequestModel;
using Project.Service;

namespace Project.Server.Controllers
{
    public interface IBaseController<T> where T: Entity
    {
        IHttpActionResult Get();
        IHttpActionResult Get(string request);
        //IHttpActionResult Get(BaseRequestModel request);
        IHttpActionResult Post(T entity);
        IHttpActionResult Put(T entity);
        IHttpActionResult Delete(string request);
    }

    public abstract class BaseController<TEntity> : ApiController, IBaseController<TEntity> where TEntity : Entity 
    {

        protected IBaseService<TEntity> Service;

        protected BaseController(IBaseService<TEntity> service)
        {
            Service = service;
        }


        public virtual IHttpActionResult Get()
        {
            var entities = Service.GetAll();

            return Ok(entities);
        }


        public virtual IHttpActionResult Get(string request)
        {
            var entity = Service.GetById(Guid.Parse(request));

            return Ok(entity);
        }


        //public virtual IHttpActionResult Get(BaseRequestModel request)
        //{
        //    TEntity entity = null;
        //    if (request.IsGuid)
        //        entity = Service.GetById(Guid.Parse(request.Id));
        //    else
        //        entity = Service.GetById(request.Id);

        //    return Ok(entity);
        //}


        public virtual IHttpActionResult Post(TEntity entity)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            entity.Created = DateTime.Now;
            entity.CreatedBy = User.Identity.GetUserId();

            var add = Service.Add(entity);

            return Ok(add);
        }


        public virtual IHttpActionResult Put(TEntity model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Modified = DateTime.Now;
            model.ModifiedBy = User.Identity.GetUserId();
            var edit = Service.Edit(model);

            return Ok(edit);
        }


        public virtual IHttpActionResult Delete(string request)
        {
            var delete = Service.Delete(Guid.Parse(request));

            return Ok(delete);
        }
    }
}
