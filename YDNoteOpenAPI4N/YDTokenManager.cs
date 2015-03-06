//作者：       minjie.zhou
// 创建时间：   2012/12/2 0:20:39
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetOpenAuth.OAuth.ChannelElements;
using DotNetOpenAuth.OAuth.Messages;

namespace YDNoteOpenAPI4N
{
    /// <summary>
    /// TokenManager 令牌管理
    /// </summary>
    public class YDTokenManager : IConsumerTokenManager
    {
        private Dictionary<string, string> _tokensAndSecrets = new Dictionary<string, string>();

        private Dictionary<string, TokenType> _tokenTypes =new Dictionary<string, TokenType>();

        /// <summary>
        /// Initializes a new instance of the <see cref="YDTokenManager"/> class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        public YDTokenManager(string consumerKey, string consumerSecret)
        {
            if (string.IsNullOrEmpty(consumerKey))
            {
                throw new ArgumentNullException("consumerKey");
            }

            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
        }

        /// <summary>
        /// Gets the consumer key.
        /// </summary>
        /// <value>The consumer key.</value>
        public string ConsumerKey { get; private set; }

        /// <summary>
        /// Gets the consumer secret.
        /// </summary>
        /// <value>The consumer secret.</value>
        public string ConsumerSecret { get; private set; }

        #region ITokenManager Members


        public string GetTokenSecret(string token)
        {
            return this._tokensAndSecrets[token];
        }

        public void StoreNewRequestToken(UnauthorizedTokenRequest request, ITokenSecretContainingMessage response)
        {
            this._tokensAndSecrets[response.Token] = response.TokenSecret;
            this._tokenTypes.Add(response.Token, TokenType.RequestToken);
        }

        public void ExpireRequestTokenAndStoreNewAccessToken(string consumerKey, string requestToken, string accessToken, string accessTokenSecret)
        {
            this._tokensAndSecrets.Remove(requestToken);
            this._tokensAndSecrets[accessToken] = accessTokenSecret;
            this._tokenTypes.Add(accessToken, TokenType.AccessToken);
        }

        /// <summary>
        /// Classifies a token as a request token or an access token.
        /// </summary>
        /// <param name="token">The token to classify.</param>
        /// <returns>Request or Access token, or invalid if the token is not recognized.</returns>
        public TokenType GetTokenType(string token)
        {
            return _tokenTypes[token];
        }

        #endregion
    }
}
