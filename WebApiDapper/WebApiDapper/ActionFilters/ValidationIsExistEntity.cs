using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApiDapper.IRepositories;

namespace WebApiDapper.ActionFilters
{
    public class ValidationIsExistEntity<T> : IAsyncActionFilter where T : class
    {
        private readonly IProductRepository<int> _productRepository;

        public ValidationIsExistEntity(IProductRepository<int> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("product", out var value) && value is T product)
            {
                var skuProperty = product.GetType().GetProperty("Sku");
                if (skuProperty != null)
                {
                    var skuValue = skuProperty.GetValue(product)?.ToString();
                    if (!string.IsNullOrEmpty(skuValue))
                    {
                        var isSkuExist = await _productRepository.IsSkuExist(skuValue, null);
                        if (isSkuExist)
                        {
                            context.Result = new ConflictObjectResult($"SKU '{skuValue}' already exists.");
                            return;
                        }
                    }
                    else
                    {
                        context.Result = new BadRequestObjectResult($"SKU is missing or invalid");
                        return;
                    }
                }
            }

            var result = await next();
        }
    }
}
