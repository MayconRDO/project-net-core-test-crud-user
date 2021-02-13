using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_server_users.Controllers
{
    public class UserController : ControllerBase
    {
        public UserController()
        {

        }

        /// <summary>
        /// Obter todos os usuários
        /// </summary>
        /// <returns>Usuários</returns>
        [HttpPost("getAll")]
        public ActionResult Get()
        {
            return Ok();
        }

        /// <summary>
        /// Obter usuário por e-mail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("get")]
        public ActionResult Get(int email)
        {
            return Ok();
        }

        /// <summary>
        /// Cadastrar usuário
        /// </summary>
        /// <returns>Usuário</returns>
        [HttpPost("add")]
        public ActionResult Add()
        {
            return Ok();
        }

        /// <summary>
        /// Alterar usuários
        /// </summary>
        /// <returns></returns>
        [HttpPut("update")]
        public ActionResult Update()
        {
            return Ok();
        }

        /// <summary>
        /// Deletar usuário por e-mail
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete")]
        public ActionResult Delete()
        {
            return Ok();
        }
    }
}
