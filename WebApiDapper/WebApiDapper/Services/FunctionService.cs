using AutoMapper;
using WebAPICoreDapper.Models;
using WebAPICoreDapper.ViewModels;
using WebApiDapper.DTOs.FunctionDTO;
using WebApiDapper.IRepositories;

namespace WebApiDapper.Services
{

    public class FunctionService
    {
        private readonly IMapper _mapper;
        private IFunctionRepository<string> _functionRepository { get; set; }
        public FunctionService(IMapper mapper, IFunctionRepository<string> functionRepository)
        {
            _mapper = mapper;
            _functionRepository = functionRepository;
        }

        public async Task<List<Function>> GetAllAsync()
        {
            var result = await _functionRepository.GetAllAsync();
            return result;
        }

        public async Task<FunctionResponseDTO?> GetByIdAsync(string functionId)
        {
            var function = await _functionRepository.GetByIdAsync(functionId);
            if (function == null) return null;
            var result = _mapper.Map<FunctionResponseDTO>(function);
            return result;
        }

        public async Task<string?> CreateAsync(FunctionRequestDTO functionRequest)
        {
            var function = _mapper.Map<Function>(functionRequest);
            return await _functionRepository.AddWithIdAsync(function);
        }

        public async Task DeleteAsync(string Id)
        {
            var function = await _functionRepository.GetByIdAsync(Id);
            if (function != null)
            {
                await _functionRepository.DeleteAsync(function.Id);
            }
        }

        public async Task UpdateAsync(string id, FunctionRequestDTO functionRequestDTO)
        {
            var result = await _functionRepository.GetByIdAsync(id);
            _mapper.Map(functionRequestDTO, result);
            await _functionRepository.UpdateAsync(result);
        }

        public async Task<List<FunctionActionViewModel>> GetFunctionWithAction()
        {
            var result = await _functionRepository.GetFunctionWithAction();
            return result;
        }
    }
}
