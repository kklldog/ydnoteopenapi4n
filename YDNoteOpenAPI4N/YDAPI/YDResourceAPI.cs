//作者：       minjie.zhou
// 创建时间：   2012/12/5 15:11:58

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using Newtonsoft.Json;
using YDNoteOpenAPI4N.DataModel;
using YDNoteOpenAPI4N.YDConsumer;

namespace YDNoteOpenAPI4N.YDAPI
{
    public class YDResourceAPI : YDBaseAPI
    {
        public YDResourceAPI(ConsumerBase consumer, string accessToken)
            : base(consumer, accessToken)
        {
        }

        /// <summary>
        /// 上传附件服务节点
        /// </summary>
        private readonly MessageReceivingEndpoint _uploadEndPoint =
            new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/resource/upload.json",
                                         HttpDeliveryMethods.PostRequest |
                                         HttpDeliveryMethods.AuthorizationHeaderRequest);

        /// <summary>
        ///上次文件
        /// </summary>
        /// <returns></returns>
        public YDResource Upload(string fileName,string contentType,Stream stream)
        {
            var filePart = MultipartPostPart.CreateFormFilePart("file", fileName, contentType, stream);
            filePart.ContentDisposition = "form-data";
            var multiparts = new List<MultipartPostPart>()
                {
                    filePart
                };

            var request = Consumer.PrepareAuthorizedRequest(_uploadEndPoint, this.AccessToken);
            IncomingWebResponse response = request.PostMultipart(Consumer.Channel.WebRequestHandler, multiparts);

            string body = response.GetResponseReader().ReadToEnd();


            var resoure = JsonConvert.DeserializeObject<YDResource>(body);
            return resoure;

        }
    }
}