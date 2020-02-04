using System;
using System.Web.Http;
using Project.Model;
using Project.Service;

namespace Project.Server.Controllers
{
    public interface IBaseSecurityController<in T> where T : class
    {
        IHttpActionResult Get();
        IHttpActionResult Get(string request);
        //IHttpActionResult Get(BaseRequestModel request);
        IHttpActionResult Post(T entity);
        IHttpActionResult Put(T entity);
        IHttpActionResult Delete(string request);
    }

    public abstract class BaseSecurityController<TEntity> : ApiController, IBaseSecurityController<TEntity> where TEntity : class
    {

        protected IBaseService<TEntity> Service;

        protected BaseSecurityController(IBaseService<TEntity> service)
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

            var add = Service.Add(entity);

            return Ok(add);
        }


        public virtual IHttpActionResult Put(TEntity model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

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