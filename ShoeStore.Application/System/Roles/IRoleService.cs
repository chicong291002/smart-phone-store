using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeStore.Application.System.Roles
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAllRoles();
    }
}
