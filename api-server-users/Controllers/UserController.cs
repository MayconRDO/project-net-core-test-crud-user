using api_server_users.DataBase.Entities;
using api_server_users.Models;
using api_server_users.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace api_server_users.Controllers
{
    /// <summary>
    /// Controlador do usuário
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IApplicationUserRepository _userRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        public UserController(IApplicationUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Obter todos os usuários
        /// </summary>
        /// <returns>Usuários</returns>
        [HttpGet("getAll")]
        public ActionResult Get()
        {
            var users = _userRepository.Get();

            if (users.Count == 0)
            {
                return NotFound("Não existem usuários cadastrados.");
            }
            return Ok(users);
        }

        /// <summary>
        /// Obter usuário por e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public ActionResult Get(string email)
        {
            var user = _userRepository.Get(email);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            return Ok(user);
        }

        /// <summary>
        /// Cadastrar usuário
        /// </summary>
        /// <returns>Usuário</returns>
        [HttpPost("add")]
        public ActionResult Add([FromBody]UserAddDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    FullName = userDTO.Name,
                    Email = userDTO.Email,
                    UserName = userDTO.Email,
                    PhoneNumber = userDTO.PhoneNumber
                };

                var result = _userRepository.Add(applicationUser, userDTO.Password);

                if (!result.Succeeded)
                {
                    List<string> errors = new List<string>();

                    foreach (var error in result.Errors)
                    {
                        errors.Add(error.Description);
                    }

                    return UnprocessableEntity(errors);
                }
                else
                {
                    return Ok(applicationUser);
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        /// <summary>
        /// Alterar usuários
        /// </summary>
        /// <returns></returns>
        [HttpPut("update")]
        public ActionResult Update([FromBody]UserUpdateDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.Get(userDTO.Email);

                if (user == null)
                {
                    return NotFound("Usuário não encontrado.");
                }
                {
                    user.FullName = userDTO.Name;
                    user.PhoneNumber = userDTO.PhoneNumber;
                }

                _userRepository.Update(user);

            }

            return Ok("Usuário alterado com sucesso.");
        }

        /// <summary>
        /// Deletar usuário por e-mail
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        public ActionResult Delete()
        {
            var user = _userRepository.Get("maycon.oliveira@gmail.com");

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _userRepository.Delete(user);

            return Ok("Usuário deletado com sucesso.");
        }
    }
}
