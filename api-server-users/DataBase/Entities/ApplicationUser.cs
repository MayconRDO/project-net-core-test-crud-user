using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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

        /// <summary>
        /// Tokens do usuário
        /// </summary>
        [ForeignKey("UserId")]
        public virtual ICollection<Token> Tokens { get; set; }
    }
}
