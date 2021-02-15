using api_server_users.DataBase.Entities;
using api_server_users.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_server_users.Repositories
{
    /// <summary>
    /// Repositorio do objeto usuário
    /// </summary>
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Construtor
        /// </summary>
        public ApplicationUserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Obter todos usuários
        /// </summary>
        /// <returns></returns>
        public List<ApplicationUser> Get()
        {
            return _userManager.Users.ToList();
        }

        /// <summary>
        /// Obter usuário por email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public ApplicationUser Get(string email)
        {
            return _userManager.FindByEmailAsync(email).Result;
        }

        /// <summary>
        /// Cadastrar usuário
        /// </summary>
        /// <param name="applicationUser">Objeto do usuário</param>
        /// <param name="password">Password do usuário</param>
        public IdentityResult Add(ApplicationUser applicationUser, string password)
        {
            return _userManager.CreateAsync(applicationUser, password).Result;
        }

        /// <summary>
        /// Alterar usuário
        /// </summary>
        /// <param name="applicationUser"></param>
        public IdentityResult Update(ApplicationUser applicationUser)
        {
            return _userManager.UpdateAsync(applicationUser).Result;

            //if (!result.Succeeded)
            //{
            //    StringBuilder sb = new StringBuilder();
            //    foreach (var error in result.Errors)
            //    {
            //        sb.Append(error.Description);
            //    }

            //    throw new Exception($"Usuário não alterado! {sb.ToString()}");
            //}
        }

        /// <summary>
        /// Deletar usuário
        /// </summary>
        /// <param name="applicationUser"></param>
        public void Delete(ApplicationUser applicationUser)
        {
            var result = _userManager.DeleteAsync(applicationUser).Result;

            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    sb.Append(error.Description);
                }

                throw new Exception($"Usuário não deletado! {sb.ToString()}");
            }
        }
    }
}
