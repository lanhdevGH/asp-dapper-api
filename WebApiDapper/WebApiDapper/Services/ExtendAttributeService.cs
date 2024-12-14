using WebApiDapper.DTOs.ExtendAttribute;
using WebApiDapper.Entities;
using WebApiDapper.IRepositories;

namespace WebApiDapper.Services
{
    public class ExtendAttributeService
    {
        private readonly IExtendAttributeRepository<int> _attributeRepository;

        public ExtendAttributeService(IExtendAttributeRepository<int> attributeRepository)
        {
            _attributeRepository = attributeRepository;
        }

        public async Task<List<ExtendAttribute>> GetAllAttribute()
        {
            var result = await _attributeRepository.GetAllAsync();
            return result;
        }

        public async Task<ExtendAttribute?> GetExtendAttributeByCodeAsync(string code)
        {
            var result = await _attributeRepository.GetAttributeByCodeAsync(code);
            return result;
        }

        public async Task<ExtendAttribute?> GetExtendAttributeByIdAsync(int id)
        {
            return await _attributeRepository.GetByIdAsync(id);
        }

        public Task<ExtendAttribute?> GetAttributeByCode(string code)
        {
            var result = _attributeRepository.GetAttributeByCodeAsync(code);
            return result;
        }

        public async Task<int?> CreateExtendAttribute(ExtendAttributeCreateRequestDTO extendAttributeDTO)
        {
            var extendAttribute = new ExtendAttribute()
            {
                Code = extendAttributeDTO.Code,
                Name = extendAttributeDTO.Name,
                DataType = extendAttributeDTO.DataType,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsActive = true
            };

            return await _attributeRepository.AddAsync(extendAttribute);
        }

        public async Task DeleteExtendAttribute(string code)
        {
            var deleteAttribute = await _attributeRepository.GetAttributeByCodeAsync(code);
            if (deleteAttribute != null)
            {
                await _attributeRepository.DeleteAsync(deleteAttribute.Id);
            }
        }

        public async Task UpdateExtendAttribute(int idExtend, ExtendAttributeCreateRequestDTO extendAttributeDTO)
        {
            var extendAttribute = new ExtendAttribute()
            {
                Id = idExtend,
                Code = extendAttributeDTO.Code,
                Name = extendAttributeDTO.Name,
                DataType = extendAttributeDTO.DataType,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                IsActive = true
            };
            await _attributeRepository.UpdateAsync(extendAttribute);
        }
    }
}
