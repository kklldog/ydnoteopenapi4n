//作者：       minjie.zhou
// 创建时间：   2012/12/1 14:18:25
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using DotNetOpenAuth.OAuth.ChannelElements;

namespace YDNoteOpenAPI4N
{
    /// <summary>
    /// OAUTH授权所需的一些基础信息
    /// </summary>
    public class YDAuthBaseInfo
    {
        public static readonly string OwnerId = "";
        public static readonly string ConsumerName = "";
        public static readonly string ConsumerKey = "";//开发者申请的KEY
        public static readonly string ConsumerSecret = "";//开发者申请的Secret

        public static readonly string BaseUrl = "";

        public static readonly ServiceProviderDescription ServiceDescription = null;//OAUTH服务提供方信息

        static YDAuthBaseInfo()
        {
            BaseUrl = "http://note.youdao.com";
            #if DEBUG
            BaseUrl = "http://sandbox.note.youdao.com";//测试沙箱基础url
            #endif
            OwnerId = "kklldog";
            ConsumerName = "AgileToDo";

            ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
            ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];

            ServiceDescription = new ServiceProviderDescription
            {
                RequestTokenEndpoint = new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/oauth/request_token",
                    HttpDeliveryMethods.AuthorizationHeaderRequest | HttpDeliveryMethods.GetRequest),
                UserAuthorizationEndpoint = new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/oauth/authorize",
                    HttpDeliveryMethods.AuthorizationHeaderRequest | HttpDeliveryMethods.GetRequest),
                AccessTokenEndpoint = new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/oauth/access_token", 
                    HttpDeliveryMethods.AuthorizationHeaderRequest | HttpDeliveryMethods.GetRequest),
                TamperProtectionElements = new ITamperProtectionChannelBindingElement[] { new HmacSha1SigningBindingElement() },
            };
        }


    }
}
