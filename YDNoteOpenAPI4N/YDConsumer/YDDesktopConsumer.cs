//作者：       minjie.zhou
// 创建时间：   2012/12/8 13:15:17
using System;
using DotNetOpenAuth.OAuth;

namespace YDNoteOpenAPI4N.YDConsumer
{
    /// <summary>
    ///有道OPEN AUTH的DESKTOP端消费者实现
    /// </summary>
    public class YDDesktopConsumer : DesktopConsumer
    {
        private YDTokenManager _tokenManager;

        public new YDTokenManager TokenManager
        {
            get { return _tokenManager; }
        }

        public YDDesktopConsumer(ServiceProviderDescription serviceProvider, YDTokenManager tokenManager)
            : base(serviceProvider, tokenManager)
        {
            _tokenManager = tokenManager;
        }



        /// <summary>
        /// 请求授权
        /// </summary>
        public static Uri RequestAuthorization(DesktopConsumer consumer, out string requestToken)
        {
            if (consumer == null)
            {
                throw new ArgumentNullException("YDDesktopConsumer");
            }
            var uri = consumer.RequestUserAuthorization(null, null, out requestToken);
            return uri;

        }
    }
}
