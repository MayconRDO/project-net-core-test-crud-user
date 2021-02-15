using api_server_users.DataBase.Entities;

namespace api_server_users.Repositories.Interfaces
{
    /// <summary>
    /// Interface do Repositório do Token
    /// </summary>
    public interface ITokenRepository
    {
        /// <summary>
        /// Cadastro do Token
        /// </summary>
        /// <param name="token">Objeto do token</param>
        void Add(Token token);

        /// <summary>
        /// Obter token
        /// </summary>
        /// <param name="refreshToken">Token renovado</param>
        /// <returns></returns>
        Token Get(string refreshToken);

        /// <summary>
        /// Altarar token
        /// </summary>
        /// <param name="token">Objeto do Token</param>
        void Update(Token token);
    }
}
