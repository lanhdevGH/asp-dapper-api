using WebApiDapper.Entities;

namespace WebApiDapper.IRepositories
{
    public interface IExtendAttributeRepository<K> : IRepository<ExtendAttribute, K>
    {
        public Task<bool> IsExistAttributeAsync(string attributeCode);

        public Task<ExtendAttribute?> GetAttributeByCodeAsync(string attributeCode);
    }

    public interface IAttributeValue<T>
    {
        public Task<List<T>> GetAttributeValueByProdIDAsync(int productID);
        public Task DeleteProductAttributeValueAsync(int productId);
    } 

    public interface IAttributeValueNVarcharRepository<K> : IRepository<AttributeValueNVarchar, K>, IAttributeValue<AttributeValueNVarchar>
    {

    }

    public interface IAttributeValueTextRepository<K> : IRepository<AttributeValueText, K>, IAttributeValue<AttributeValueText>
    {

    }

    public interface IAttributeValueIntRepository<K> : IRepository<AttributeValueInt, K>, IAttributeValue<AttributeValueInt>
    {

    }

    public interface IAttributeValueDecimalRepository<K> : IRepository<AttributeValueDecimal, K>, IAttributeValue<AttributeValueDecimal>
    {

    }

    public interface IAttributeValueDateTimeRepository<K> : IRepository<AttributeValueDateTime, K>, IAttributeValue<AttributeValueDateTime>  
    {

    }
}
