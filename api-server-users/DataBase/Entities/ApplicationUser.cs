using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_server_users.DataBase.Entities
{
    /// <summary>
    /// Objeto usuário
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Nome completo do usuário
        /// </summary>
        public string FullName { get; set; }
    }
}
