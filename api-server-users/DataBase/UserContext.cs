using api_server_users.DataBase.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_server_users.DataBase
{
    /// <summary>
    /// Contexto do Usuário
    /// </summary>
    public class UserContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Construtor do usuário
        /// </summary>
        /// <param name="options"></param>
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        // Aqui abaixo vão as demais tabelas....
        /// <summary>
        /// Tabela ApplicationUsers
        /// </summary>
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /// <summary>
        /// Tabela Tokens 
        /// </summary>
        public DbSet<Token> Tokens { get; set; }
    }
}
