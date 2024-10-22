using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDapper.Contracts;
using WebApiDapper.DbContext;
using WebApiDapper.IRepositories;
using WebApiDapper.IRepositories.Impl;

namespace WebApiDapper.ActionFilters
{
    public class ValidationNotExistEntityAttribute<T> : IAsyncActionFilter where T : class
    {
        private readonly IRepository<T> _repository;
        public ValidationNotExistEntityAttribute(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            int id = 0;
            if (context.ActionArguments.ContainsKey("id"))
            {
                id = (int)context.ActionArguments["id"];
            }
            else
            {
                context.Result = new BadRequestObjectResult("Bad id parameter");
                return;
            }

            var entity = await _repository.GetById(id);
            if (entity == null)
            {
                context.Result = new NotFoundResult();
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
