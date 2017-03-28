using System.Web.Http;
using Core.Data;
using Core.Interfaces;

namespace ResourceServer.Controllers
{
    public  class DataApiController<TEntity, TService> : ApiController where TEntity : class, IEntity, new() where TService : IDataService<TEntity>
    {
        protected readonly TService Service;

        public DataApiController(TService service)
        {
            Service = service;
        }
    }
}