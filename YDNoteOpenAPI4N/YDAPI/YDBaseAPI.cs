//作者：       minjie.zhou
// 创建时间：   2012/12/4 1:13:29
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;
using YDNoteOpenAPI4N.YDConsumer;

namespace YDNoteOpenAPI4N.YDAPI
{
    public class YDBaseAPI
    {
             /// <summary>
        /// 消费者
        /// </summary>
        private ConsumerBase _consumer;
        /// <summary>
        /// 令牌
        /// </summary>
        private IConsumerTokenManager _tokenManager;

        private string _accessToken;

        public YDBaseAPI(ConsumerBase consumer,string accessToken)
        {
            _consumer = consumer;
            _tokenManager = consumer.TokenManager;
            _accessToken = accessToken;
        }

        public YDTokenManager TokenManager
        {
            get { return _tokenManager as YDTokenManager; }
        }

        public ConsumerBase Consumer
        {
            get { return _consumer; }
        }

        public string AccessToken
        {
            get { return _accessToken; }
        }
    }
}
