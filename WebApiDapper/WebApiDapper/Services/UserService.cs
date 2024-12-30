using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Identity;
using WebAPICoreDapper.Models;
using WebApiDapper.DbContext;
using WebApiDapper.DTOs.UserDTO;

namespace WebApiDapper.Services
{
    public class UserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DapperDBContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, DapperDBContext context, IMapper mapper)
        {
            _dbContext = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserResponseDTO>> GetAllUser()
        {
            var result = new List<UserResponseDTO>();
            using (var conn = _dbContext.CreateConnection())
            {
                var paramaters = new DynamicParameters();
                var queryResult = await conn.QueryAsync<UserResponseDTO>("User_GetAll", paramaters, null, null, System.Data.CommandType.StoredProcedure);
                result.AddRange(queryResult);
            }
            return result;
        }

        public async Task<AppUser?> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> CreateUser(UserRequestDTO userRequestDTO)
        {
            var user = _mapper.Map<AppUser>(userRequestDTO);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userRequestDTO.Password);
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> UpdateUser(Guid id, UserUpdateDTO userUpdate)
        {
            var existUser = await GetUserById(id.ToString());
            if (existUser == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            }
            _mapper.Map(userUpdate, existUser);
            return await _userManager.UpdateAsync(existUser);
        }

        public async Task<IdentityResult> DeleteUser(string idUser)
        {
            var user = await _userManager.FindByIdAsync(idUser);
            return await _userManager.DeleteAsync(user);
        }
    }
}
