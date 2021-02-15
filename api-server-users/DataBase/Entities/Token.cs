using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_server_users.DataBase.Entities
{
    /// <summary>
    /// Objeto do Token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Identificador do Token
        /// </summary>
        [Key]
        public int Id { get; set; }
        
        /// <summary>
        /// Token atualizado
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Identificador do usuário
        /// </summary>
        [ForeignKey("User")]
        public string UserId { get; set; }

        /// <summary>
        /// Objeto do usuário
        /// </summary>
        public ApplicationUser User { get; set; }

        /// <summary>
        /// Flag para indicar se o token foi utilizado ou não.
        /// </summary>
        public bool Used { get; set; }

        /// <summary>
        /// Data de expiração do token
        /// </summary>
        public DateTime ExpirationToken { get; set; }

        /// <summary>
        /// Data de expiração do token atualizado
        /// </summary>
        public DateTime ExpirationRefreshToken { get; set; }

        /// <summary>
        /// Data de criação do token
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Data de modificação do token
        /// </summary>
        public DateTime? DateModifield { get; set; }
    }
}
