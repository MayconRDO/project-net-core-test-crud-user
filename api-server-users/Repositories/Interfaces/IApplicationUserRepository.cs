using api_server_users.DataBase.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_server_users.Repositories.Interfaces
{
    /// <summary>
    /// Interface do objeto usuário
    /// </summary>
    public interface IApplicationUserRepository
    {
        /// <summary>
        /// Obter todos os usuários
        /// </summary>
        /// <returns></returns>
        List<ApplicationUser> Get();
        
        /// <summary>
        /// Obter usuário por email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns></returns>
        ApplicationUser Get(string email);

        /// <summary>
        /// Cadastrar usuário
        /// </summary>
        /// <param name="applicationUser">Objeto usuário</param>
        /// <param name="password">Senha</param>
        IdentityResult Add(ApplicationUser applicationUser, string password);

        /// <summary>
        /// Alterar usuário
        /// </summary>
        /// <param name="applicationUser">Objeto do usuário</param>
        IdentityResult Update(ApplicationUser applicationUser);
        
        /// <summary>
        /// Deletar usuário
        /// </summary>
        /// <param name="applicationUser">Objeto do usuário</param>
        void Delete(ApplicationUser applicationUser);
    }
}
