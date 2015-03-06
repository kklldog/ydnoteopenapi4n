//作者：       minjie.zhou
// 创建时间：   2012/12/3 23:11:42
using System;
using System.Collections.Generic;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using Newtonsoft.Json;
using YDNoteOpenAPI4N.DataModel;
using System.Linq.Expressions;
using System.Linq;

namespace YDNoteOpenAPI4N.YDAPI
{
    /// <summary>
    /// 笔记本操作类
    /// </summary>
    public class YDNoteBookAPI:YDBaseAPI
    {
        /// <summary>
        /// 获取所有的笔记本列表的服务节点
        /// </summary>
        private  readonly MessageReceivingEndpoint _getAllNoteBooksEndpoint =
           new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/notebook/all.json", HttpDeliveryMethods.PostRequest);
        /// <summary>
        /// 获取笔记本下的所有笔记PATH的服务节点
        /// </summary>
        private  readonly MessageReceivingEndpoint _getNotesInBookEndpoint =
           new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/notebook/list.json", HttpDeliveryMethods.PostRequest);
        /// <summary>
        /// 创建笔记本的服务节点
        /// </summary>
        private  readonly MessageReceivingEndpoint _createNoteBookEndPoint=
            new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/notebook/create.json", HttpDeliveryMethods.PostRequest);
        /// <summary>
        /// 删除笔记本的服务节点
        /// </summary>
        private  readonly MessageReceivingEndpoint _deleteNoteBookEndPoint=
            new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/notebook/delete.json", HttpDeliveryMethods.PostRequest);

        public YDNoteBookAPI(ConsumerBase consumer,string accessToken)
            : base(consumer, accessToken)
        {
        }

        /// <summary>
        /// 获取所有笔记
        /// </summary>
        /// <param name="consumer">消费者</param>
        /// <param name="tokenManager">令牌</param>
        /// <returns></returns> 
        public  IList<YDNoteBook> GetAllNoteBooks()
        {
            var extraData = new Dictionary<string, string>();

            var request = Consumer.PrepareAuthorizedRequest(_getAllNoteBooksEndpoint, this.AccessToken, extraData);

            var response = Consumer.Channel.WebRequestHandler.GetResponse(request);
            string body = response.GetResponseReader().ReadToEnd();
            var noteBooks = JsonConvert.DeserializeObject<IList<YDNoteBook>>(body, new YDDateTimeConverter4s());

            return noteBooks;
        }

        /// <summary>
        /// 获取笔记本下的笔记列表
        /// </summary>
        /// <param name="consumer"></param>
        /// <param name="tokenManager"></param>
        /// <param name="bookPath">笔记本路径</param>
        /// <returns></returns>
        public  IList<YDNote> GetNotesInBook(string bookPath)
        {
            var extraData = new Dictionary<string, string>()
                                {
                                    { "notebook", bookPath }
                                };

            var noteApi = new YDNoteAPI(this.Consumer,this.AccessToken);

            var request = Consumer.PrepareAuthorizedRequest(_getNotesInBookEndpoint, this.AccessToken, extraData);

            var response = Consumer.Channel.WebRequestHandler.GetResponse(request);
            string body = response.GetResponseReader().ReadToEnd();
            var notes = JsonConvert.DeserializeObject<IList<string>>(body);
            var listNote = new List<YDNote>();
            foreach (var path in notes)
            {
                var note = noteApi.GetNote(path);
                listNote.Add(note);
            }
            return listNote;
        }
        /// <summary>
        /// 创建笔记本
        /// </summary>
        /// <param name="consumer"></param>
        /// <param name="tokenManager"></param>
        /// <param name="bookName">笔记本名称</param>
        /// <returns></returns>
        public  YDNoteBook CreateNoteBook(string bookName)
        {
            var extraData = new Dictionary<string, string>()
                                {
                                    { "name", bookName }
                                };

            var request = Consumer.PrepareAuthorizedRequest(_createNoteBookEndPoint, this.AccessToken, extraData);

            var response = Consumer.Channel.WebRequestHandler.GetResponse(request);
            string body = response.GetResponseReader().ReadToEnd();
            var newBook = JsonConvert.DeserializeObject<YDNoteBook>(body,new YDDateTimeConverter4s());
            return newBook;
        }

        /// <summary>
        /// 删除笔记本 成功无任何返回，失败返回一个异常
        /// </summary>
        /// <param name="consumer"></param>
        /// <param name="tokenManager"></param>
        /// <param name="bookPath">笔记本路径</param>
        public  void DeleteNoteBook( string bookPath)
        {
            var extraData = new Dictionary<string, string>()
                                {
                                    { "notebook", bookPath }
                                };

            var request =  Consumer.PrepareAuthorizedRequest(_deleteNoteBookEndPoint, this.AccessToken, extraData);
            try
            {
               Consumer.Channel.WebRequestHandler.GetResponse(request);
            }
            catch
            {
               throw  new Exception("DELETE NOTE BOOK FAILED");
            }
        }

    }
}
