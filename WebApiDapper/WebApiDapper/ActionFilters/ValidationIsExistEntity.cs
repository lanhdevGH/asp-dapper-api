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
                // Lấy thuộc tính SKU
                var skuProperty = product.GetType().GetProperty("Sku");
                var idProperty = product.GetType().GetProperty("Id"); // Lấy Id nếu có

                if (skuProperty != null)
                {
                    var skuValue = skuProperty.GetValue(product)?.ToString();
                    var idValue = idProperty?.GetValue(product) as int?;

                    if (!string.IsNullOrEmpty(skuValue))
                    {
                        // Kiểm tra SKU đã tồn tại
                        var isSkuExist = await _productRepository.IsSkuExist(skuValue, idValue);
                        if (isSkuExist)
                        {
                            context.Result = new ConflictObjectResult($"SKU '{skuValue}' already exists.");
                            return;
                        }
                    }
                    else
                    {
                        context.Result = new BadRequestObjectResult("SKU is missing or invalid");
                        return;
                    }
                }
            }

            // Tiếp tục nếu không có lỗi
            await next();
        }

    }
}
