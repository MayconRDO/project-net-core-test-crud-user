using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_server_users.Models
{
    /// <summary>
    /// Visualização do objeto Token
    /// </summary>
    public class TokenDTO
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// Data de expiração
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Data de renovação
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Data de expiração da renovação
        /// </summary>
        public DateTime ExpirationRefreshToken { get; set; }
    }
}
