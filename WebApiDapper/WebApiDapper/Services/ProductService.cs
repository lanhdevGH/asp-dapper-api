using WebApiDapper.DTOs.ProductDTO;
using WebApiDapper.Entities;
using WebApiDapper.IRepositories;

namespace WebApiDapper.Services
{
    public class ProductService
    {
        private readonly IProductRepository<int> _productRepository;
        private readonly IExtendAttributeRepository<int> _attributeRepository;
        private readonly IAttributeValueNVarcharRepository<int> _attributeValueNVarchar;
        private readonly IAttributeValueTextRepository<int> _attributeValueText;
        private readonly IAttributeValueIntRepository<int> _attributeValueInt;
        private readonly IAttributeValueDecimalRepository<int> _attributeValueDecimal;
        private readonly IAttributeValueDateTimeRepository<int> _attributeValueDateTime;

        public ProductService(IProductRepository<int> productRepository,
            IExtendAttributeRepository<int> attributeRepository,
            IAttributeValueNVarcharRepository<int> attributeValueNVarcharRepository,
            IAttributeValueTextRepository<int> attributeValueTextRepository,
            IAttributeValueIntRepository<int> attributeValueIntRepository,
            IAttributeValueDecimalRepository<int> attributeValueDecimalRepository,
            IAttributeValueDateTimeRepository<int> attributeValueDateTimeRepository)
        {
            _attributeRepository = attributeRepository;
            _productRepository = productRepository;
            _attributeValueNVarchar = attributeValueNVarcharRepository;
            _attributeValueText = attributeValueTextRepository;
            _attributeValueInt = attributeValueIntRepository;
            _attributeValueDecimal = attributeValueDecimalRepository;
            _attributeValueDateTime = attributeValueDateTimeRepository;
        }

        public async Task<int> CreateProductAsync(ProductCreateRequestDTO product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Content = product.Content,
                ImageUrl = product.ImageUrl,
                ImageList = product.ImageList,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                Sku = product.Sku,
                CategoryId = product.CategoryId,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsActive = true,
                ViewCount = 0,
                RateCount = 0,
                RateTotal = 0,
            };

            var newProductId = await _productRepository.AddAsync(newProduct);
            if (newProductId == 0) throw new Exception("");
            await CreateExtendAttributeProductAsync(newProductId, product.ExtendAttributes);
            return newProductId;
        }

        public async Task<List<ProductCreateResponseDTO>> GetAllProductAsync()
        {
            var result = new List<ProductCreateResponseDTO>();
            var products = await _productRepository.GetAllAsync();
            foreach (var prd in products)
            {
                var product = new ProductCreateResponseDTO()
                {
                    Id = prd.Id,
                    Name = prd.Name,
                    Description = prd.Description,
                    Content = prd.Content,
                    Price = prd.Price,
                    DiscountPrice = prd.DiscountPrice,
                    ImageUrl = prd.ImageUrl,
                    ImageList = prd.ImageList,
                    RateTotal = prd.RateTotal,
                    CategoryId = prd.CategoryId,
                    CreateDate = prd.CreateDate,
                    UpdateDate = prd.UpdateDate,
                    Sku = prd.Sku,
                    ViewCount = prd.ViewCount,
                    IsActive = prd.IsActive,
                    RateCount = prd.RateCount
                };

                product.ExtendAttributes = await GetExtendAttributeProductAsync(prd.Id);
                result.Add(product);
            }
            return result;
        }

        public async Task<ProductCreateResponseDTO> GetProductById(int productId)
        {
            var result = new ProductCreateResponseDTO();
            var product = await _productRepository.GetByIdAsync(productId);
            result.Id = product.Id;
            result.Name = product.Name;
            result.Description = product.Description;
            result.Content = product.Content;
            result.Price = product.Price;
            result.DiscountPrice = product.DiscountPrice;
            result.ImageUrl = product.ImageUrl;
            result.ImageList = product.ImageList;
            result.RateTotal = product.RateTotal;
            result.CategoryId = product.CategoryId;
            result.CreateDate = product.CreateDate;
            result.UpdateDate = product.UpdateDate;
            result.Sku = product.Sku;
            result.ViewCount = product.ViewCount;
            result.IsActive = product.IsActive;
            result.RateCount = product.RateCount;
            result.ExtendAttributes = await GetExtendAttributeProductAsync(productId);
            return result;
        }

        public async Task<int> UpdateProduct(int productId, ProductUpdateRequestDTO productUpdateDTO)
        {
            var productUpdate = new Product();
            productUpdate.Id = productId;
            productUpdate.Name = productUpdateDTO.Name;
            productUpdate.Description = productUpdateDTO.Description;
            productUpdate.Content = productUpdateDTO.Content;
            productUpdate.Price = productUpdateDTO.Price;
            productUpdate.DiscountPrice = productUpdateDTO.DiscountPrice;
            productUpdate.ImageUrl = productUpdateDTO.ImageUrl;
            productUpdate.ImageList = productUpdateDTO.ImageList;
            productUpdate.RateTotal = productUpdateDTO.RateTotal;
            productUpdate.CategoryId = productUpdateDTO.CategoryId;
            productUpdate.CreateDate = productUpdateDTO.CreateDate;
            productUpdate.UpdateDate = DateTime.Now;
            productUpdate.Sku = productUpdateDTO.Sku;
            productUpdate.ViewCount = productUpdateDTO.ViewCount;
            productUpdate.IsActive = productUpdateDTO.IsActive;
            productUpdate.RateCount = productUpdateDTO.RateCount;

            await _productRepository.UpdateAsync(productUpdate);
            await DeleteExtendAttributeProductValAsync(productId);
            if (productUpdateDTO.ExtendAttributes != null)
            {
                await CreateExtendAttributeProductAsync(productId, productUpdateDTO.ExtendAttributes);
            }
            return productId;
        }

        public async Task DeleteProductByIdAsync(int productId)
        {
            var productDelete = _productRepository.GetByIdAsync(productId);
            if (productDelete == null)
            {
                throw new Exception($"Product Id = '{productId}' not found in database.");
            }

            await DeleteExtendAttributeProductValAsync(productId);
            await _productRepository.DeleteAsync(productId);
        }

        #region ExtendAttribute
        public async Task<Dictionary<string, string>> GetExtendAttributeProductAsync(int productId)
        {
            Dictionary<string, string> attributeValues = new Dictionary<string, string>();

            async Task AddAttributes<T>(IEnumerable<T> attributes, Func<T, int> getAttributeId, Func<T, object> getValue)
            {
                foreach (var attr in attributes)
                {
                    var attribute = await _attributeRepository.GetByIdAsync(getAttributeId(attr));
                    if (attribute != null)
                    {
                        attributeValues[attribute.Code] = getValue(attr).ToString();
                    }
                }
            }

            var nvarcharAttributes = await _attributeValueNVarchar.GetAttributeValueByProdIDAsync(productId);
            await AddAttributes(nvarcharAttributes, attr => attr.AttributeId, attr => attr.Value);

            // Lấy các thuộc tính Text và thêm vào dictionary
            var textAttributes = await _attributeValueText.GetAttributeValueByProdIDAsync(productId);
            await AddAttributes(textAttributes, attr => attr.AttributeId, attr => attr.Value);

            // Lấy các thuộc tính Int và thêm vào dictionary
            var intAttributes = await _attributeValueInt.GetAttributeValueByProdIDAsync(productId);
            await AddAttributes(intAttributes, attr => attr.AttributeId, attr => attr.Value);

            // Lấy các thuộc tính Decimal và thêm vào dictionary
            var decimalAttributes = await _attributeValueDecimal.GetAttributeValueByProdIDAsync(productId);
            await AddAttributes(decimalAttributes, attr => attr.AttributeId, attr => attr.Value);

            // Lấy các thuộc tính DateTime và thêm vào dictionary
            var dateTimeAttributes = await _attributeValueDateTime.GetAttributeValueByProdIDAsync(productId);
            await AddAttributes(dateTimeAttributes, attr => attr.AttributeId, attr =>
                Convert.ToDateTime(attr.Value).ToString("yyyy-MM-dd HH:mm:ss"));

            return attributeValues;
        }

        public async Task CreateExtendAttributeProductAsync(int productId, Dictionary<string, string>? extendAttributes)
        {
            if (extendAttributes != null)
            {
                foreach (var (attributeCode, value) in extendAttributes)
                {
                    var attributeRecord = await _attributeRepository.GetAttributeByCodeAsync(attributeCode);

                    if (attributeRecord == null)
                    {
                        throw new Exception($"Attribute '{attributeCode}' not found in database.");
                    }

                    switch (attributeRecord.DataType)
                    {
                        case Enums.AttributeDBType.NVarcharType:
                            var NVarcharAttribute = new AttributeValueNVarchar()
                            {
                                AttributeId = attributeRecord.Id,
                                ProductId = productId,
                                Value = value
                            };
                            await _attributeValueNVarchar.AddAsync(NVarcharAttribute);
                            break;
                        case Enums.AttributeDBType.TextType:
                            var TextAttribute = new AttributeValueText()
                            {
                                AttributeId = attributeRecord.Id,
                                ProductId = productId,
                                Value = value
                            };
                            await _attributeValueText.AddAsync(TextAttribute);
                            break;
                        case Enums.AttributeDBType.IntType:
                            var IntAttribute = new AttributeValueInt()
                            {
                                AttributeId = attributeRecord.Id,
                                ProductId = productId,
                                Value = Convert.ToInt32(value)
                            };
                            await _attributeValueInt.AddAsync(IntAttribute);
                            break;
                        case Enums.AttributeDBType.DecimalType:
                            var DecimalAttribute = new AttributeValueDecimal()
                            {
                                AttributeId = attributeRecord.Id,
                                ProductId = productId,
                                Value = Convert.ToDecimal(value)
                            };
                            await _attributeValueDecimal.AddAsync(DecimalAttribute);
                            break;
                        case Enums.AttributeDBType.DateTimeType:
                            var DateTimeAttribute = new AttributeValueDateTime()
                            {
                                AttributeId = attributeRecord.Id,
                                ProductId = productId,
                                Value = Convert.ToDateTime(value)
                            };
                            await _attributeValueDateTime.AddAsync(DateTimeAttribute);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public async Task DeleteExtendAttributeProductValAsync(int productId)
        {
            await _attributeValueNVarchar.DeleteProductAttributeValueAsync(productId);
            await _attributeValueText.DeleteProductAttributeValueAsync(productId);
            await _attributeValueDateTime.DeleteProductAttributeValueAsync(productId);
            await _attributeValueInt.DeleteProductAttributeValueAsync(productId);
            await _attributeValueDecimal.DeleteProductAttributeValueAsync(productId);
        }
        #endregion
    }
}
