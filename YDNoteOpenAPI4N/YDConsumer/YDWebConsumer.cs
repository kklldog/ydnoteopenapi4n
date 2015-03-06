//作者：       minjie.zhou
// 创建时间：   2012/12/2 23:45:09
using System;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;

namespace YDNoteOpenAPI4N.YDConsumer
{
    /// <summary>
    /// 有道OPEN AUTH的web端消费者实现
    /// </summary>
    public class YDWebConsumer : WebConsumer
    {
        public YDWebConsumer(ServiceProviderDescription serviceProvider, YDTokenManager tokenManager)
            : base(serviceProvider, tokenManager)
        {
        }

        /// <summary>
        /// 请求授权
        /// </summary>
        /// <param name="consumer"></param>
        public static void RequestAuthorization(WebConsumer consumer)
        {
            if (consumer == null)
            {
                throw new ArgumentNullException("YDWebConsumer");
            }

            Uri callback = GetCallbackUrlFromContext();
            var request = consumer.PrepareRequestUserAuthorization(callback, null, null);
            consumer.Channel.Send(request);
        }

        /// <summary>
        /// 获取CALLBACKURL
        /// </summary>
        /// <returns></returns>
        internal static Uri GetCallbackUrlFromContext()
        {
            Uri callback = MessagingUtilities.GetRequestUrlFromContext().StripQueryArgumentsWithPrefix("oauth_");
            return callback;
        }
    }
}
