//作者：       minjie.zhou
// 创建时间：   2012/12/3 23:11:03
using System.Collections.Generic;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using YDNoteOpenAPI4N.DataModel;
using YDNoteOpenAPI4N.YDConsumer;

namespace YDNoteOpenAPI4N.YDAPI
{
    /// <summary>
    /// 用户操作API
    /// </summary>
    public class YDUserAPI:YDBaseAPI 
    {
        /// <summary>
        /// 获取用户信息终结点
        /// </summary>
        private  readonly MessageReceivingEndpoint _getUserInfoEndpoint =
            new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl+"/yws/open/user/get.json", HttpDeliveryMethods.GetRequest);

        public YDUserAPI(ConsumerBase consumer,string accessToken)
            : base(consumer,accessToken)
        {
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public  YDUser GetUserInfo()
        {
            var extraData = new Dictionary<string, string>();

            var request = Consumer.PrepareAuthorizedRequest(_getUserInfoEndpoint, this.AccessToken, extraData);

            var response = Consumer.Channel.WebRequestHandler.GetResponse(request);
            string body = response.GetResponseReader().ReadToEnd();
            var ydUser = JsonConvert.DeserializeObject<YDUser>(body, new YDDateTimeConverter4ms());

            return ydUser;
        }

    }
}
