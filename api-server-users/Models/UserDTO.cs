using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_server_users.Models
{
    /// <summary>
    /// Objeto de visualização do usuário
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// Email 
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        /// <summary>
        /// Senha
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Confirmação de Senha
        /// </summary>
        [Required]
        [Compare("Password")]
        public string PasswordConfirmation { get; set; }
    }
}
