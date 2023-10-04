using SmartPhoneStore.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.Application.System.Roles
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAllRoles();
    }
}
