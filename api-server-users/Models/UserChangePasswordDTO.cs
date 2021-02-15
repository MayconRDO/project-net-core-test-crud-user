using System.ComponentModel.DataAnnotations;

namespace api_server_users.Models
{
    /// <summary>
    /// Objeto de visualização da troca de senha do usuário
    /// </summary>
    public class UserChangePasswordDTO
    {
        /// <summary>
        /// Email 
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "O e-mail é obrigatório")]
        public string Email { get; set; }

        /// <summary>
        /// Senha Atual
        /// </summary>
        [DataType(DataType.Password)]
        public string PasswordCurrent { get; set; }

        /// <summary>
        /// Senha Nova
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string PasswordNew { get; set; }

        /// <summary>
        /// Confirmação de Senha
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("PasswordNew")]
        public string PasswordNewConfirmation { get; set; }
    }
}
