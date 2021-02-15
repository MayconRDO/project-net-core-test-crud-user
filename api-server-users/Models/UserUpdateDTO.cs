using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_server_users.Models
{
    /// <summary>
    /// View de update do objeto Usuário
    /// </summary>
    public class UserUpdateDTO
    {
        /// <summary>
        /// Email 
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        /// <summary>
        /// Número Telefone
        /// </summary>
        public string PhoneNumber { get; set; }

    }
}
