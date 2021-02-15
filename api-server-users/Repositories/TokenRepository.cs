using api_server_users.DataBase;
using api_server_users.DataBase.Entities;
using api_server_users.Repositories.Interfaces;
using System.Linq;

namespace api_server_users.Repositories
{
    /// <summary>
    /// Repositório do token
    /// </summary>
    public class TokenRepository : ITokenRepository
    {
        private readonly UserContext _context;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="context"></param>
        public TokenRepository(UserContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Cadastrar TOken
        /// </summary>
        /// <param name="token">Token</param>
        public void Add(Token token)
        {
            _context.Tokens.Add(token);
            _context.SaveChanges();
        }


        /// <summary>
        /// Obter token
        /// </summary>
        /// <param name="refreshToken">Token Renovado</param>
        /// <returns>Token Renovado</returns>
        public Token Get(string refreshToken)
        {
            return _context.Tokens.FirstOrDefault(t => t.RefreshToken == refreshToken && !t.Used);
        }

        /// <summary>
        /// Alterar Token
        /// </summary>
        /// <param name="token">token</param>
        public void Update(Token token)
        {
            _context.Tokens.Update(token);
            _context.SaveChanges();
        }
    }
}
