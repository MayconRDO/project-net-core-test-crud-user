using api_server_users.DataBase.Entities;
using api_server_users.Models;
using api_server_users.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly ITokenRepository _tokenRepository;

        /// <summary>
        /// Construtor
        /// </summary>
        public UserController(IApplicationUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
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
        /// Realizar login
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Senha</param>
        /// <returns></returns>
        [HttpPost("login")]
        public ActionResult Login([Required]string email, [Required]string password)
        {
            var applicationUser = _userRepository.Get(email);

            if (applicationUser == null)
            {
                return NotFound("Usuário ou senha inválida.");
            }

            if (!_userRepository.CheckPassword(applicationUser, password))
            {
                return NotFound("Usuário ou senha inválida.");
            }

            // Gera token
            return Ok(GenerateToken(applicationUser));

        }

        /// <summary>
        /// Alterar senha - Necessário Token (obter no Login)
        /// </summary>
        /// <param name="userDTO">Modelo de input de alteração de senha</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("changePassword")]
        public ActionResult ChangePassword([FromBody]UserChangePasswordDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.Get(userDTO.Email);

                if (user == null)
                {
                    return NotFound("Usuário não encontrado.");
                }

                if (!_userRepository.CheckPassword(user, userDTO.PasswordCurrent))
                {
                    return NotFound("Senha atual inválida.");
                }

                user.PasswordHash = userDTO.PasswordNew;

                var result = _userRepository.ChangePassword(user, userDTO.PasswordNew);

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
                    return Ok("Senha alterada com sucesso.");
                }

            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        /// <summary>
        /// Alterar usuário - Necessário Token (obter no Login)
        /// </summary>
        /// <returns></returns>
        [Authorize]
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
        /// Obter usuário por e-mail - Necessário Token (obter no Login)
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("get")]
        public ActionResult Get([Required]string email)
        {
            var user = _userRepository.Get(email);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }
            return Ok(user);
        }

        /// <summary>
        /// Obter todos os usuários - Necessário Token (obter no Login)
        /// </summary>
        /// <returns>Usuários</returns>
        [Authorize]
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
        /// Deletar usuário por e-mail - Necessário Token (obter no Login)
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("delete")]
        public ActionResult Delete([Required]string email)
        {
            var user = _userRepository.Get(email);

            if (user == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _userRepository.Delete(user);

            return Ok("Usuário deletado com sucesso.");
        }

        /// <summary>
        /// Geração do token
        /// </summary>
        /// <param name="user">Objeto do usuário</param>
        /// <returns></returns>
        private TokenDTO BuildToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("key-api-jwt-tasks"));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.Now.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(issuer: null,
                                                          audience: null,
                                                          claims: claims,
                                                          expires: exp,
                                                          signingCredentials: sign);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var refreshToken = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var expRefreshToken = DateTime.Now.AddHours(2);

            var tokenDTO = new TokenDTO
            {
                Token = tokenString,
                Expiration = exp,
                ExpirationRefreshToken = expRefreshToken,
                RefreshToken = refreshToken
            };

            return tokenDTO;
        }

        /// <summary>
        /// Gerar e cadastrar Token
        /// </summary>
        /// <param name="applicationUser"></param>
        /// <returns></returns>
        private ActionResult GenerateToken(ApplicationUser applicationUser)
        {
            var token = BuildToken(applicationUser);

            var newToken = new Token()
            {
                RefreshToken = token.RefreshToken,
                ExpirationToken = token.Expiration,
                ExpirationRefreshToken = token.ExpirationRefreshToken,
                User = applicationUser,
                DateCreated = DateTime.Now,
                Used = false
            };

            _tokenRepository.Add(newToken);

            return Ok(token);
        }
    }
}
