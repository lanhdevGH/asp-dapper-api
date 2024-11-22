using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDapper.Contracts;
using WebApiDapper.DbContext;
using WebApiDapper.IRepositories;
using WebApiDapper.IRepositories.Impl;

namespace WebApiDapper.ActionFilters
{
    public class ValidationNotExistEntityAttribute<T, K> : IAsyncActionFilter where T : class
    {
        private readonly IRepository<T, K> _repository;
        public ValidationNotExistEntityAttribute(IRepository<T, K> repository)
        {
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (!context.ActionArguments.TryGetValue("id", out var id) || id is not K entityId)
            {
                context.Result = new BadRequestObjectResult("Invalid or missing 'id' parameter");
                return;
            }

            var entity = await _repository.GetByIdAsync(entityId);
            if (entity == null)
            {
                context.Result = new NotFoundResult();
                return;
            }
            else
            {
                context.HttpContext.Items.Add("Entity", entity);
            }

            var result = await next();

            // code after Action excution
        }
    }
}
