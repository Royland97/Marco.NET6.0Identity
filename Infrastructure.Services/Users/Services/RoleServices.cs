using AutoMapper;
using Core.DataAccess.Users;
using Core.Domain.Users;
using Infrastructure.Services.Users.IServices;
using Infrastructure.Services.Users.Models;

namespace Infrastructure.Services.Users.Services
{
    /// <summary>
    /// Role Services
    /// </summary>
    public class RoleServices: IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleServices"/> class.
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="mapper"></param>
        public RoleServices(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Saves a Role
        /// </summary>
        /// <param name="roleModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task SaveRoleAsync(RoleModel roleModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = _mapper.Map<Role>(roleModel);
            await _roleRepository.SaveRoleAsync(role);
        }

        /// <summary>
        /// Updates a Role
        /// </summary>
        /// <param name="roleModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task UpdateRoleAsync(RoleModel roleModel, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var role = _mapper.Map<Role>(roleModel);
            await _roleRepository.UpdateRoleAsync(role);
        }

        /// <summary>
        /// Deletes a Role
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DeleteRoleAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _roleRepository.DeleteRoleAsync(id);
        }

        /// <summary>
        /// Gets All Roles
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<List<RoleModelList>> GetAllRolesAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var result = await _roleRepository.GetAllRolesAsync();
            return _mapper.Map<List<RoleModelList>>(result);
        }

        /// <summary>
        /// Gets a Role by it's Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RoleModelList> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            Role role = await _roleRepository.GetRoleByIdAsync(id);
            return _mapper.Map<RoleModelList>(role);
        }
    }
}
