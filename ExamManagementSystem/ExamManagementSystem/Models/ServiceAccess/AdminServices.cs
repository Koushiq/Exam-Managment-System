using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExamManagementSystem.Models.ServiceAccess
{
    public class AdminServices
    {
        public static bool HasPermission(int adminPermissionValue, AdminPermissions requiredPermission)
        {
            return adminPermissionValue.CheckBit((int)requiredPermission);
        }
    }
}