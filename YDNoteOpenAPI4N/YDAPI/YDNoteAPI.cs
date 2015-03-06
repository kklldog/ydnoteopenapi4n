//作者：       minjie.zhou
// 创建时间：   2012/12/3 23:11:57
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OAuth;
using Newtonsoft.Json;
using YDNoteOpenAPI4N.DataModel;
using YDNoteOpenAPI4N.YDConsumer;

namespace YDNoteOpenAPI4N.YDAPI
{
    public class YDNoteAPI:YDBaseAPI
    {
        public YDNoteAPI(ConsumerBase consumer,string accessToken)
            : base(consumer, accessToken)
        {

        }
        /// <summary>
        /// 创建笔记服务节点
        /// </summary>
        private readonly MessageReceivingEndpoint _createNoteEndPoint =
        new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/note/create.json", HttpDeliveryMethods.PostRequest | HttpDeliveryMethods.AuthorizationHeaderRequest);

        /// <summary>
        /// 获取笔记服务节点
        /// </summary>
        private readonly MessageReceivingEndpoint _getNoteEndPoint =
        new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/note/get.json", HttpDeliveryMethods.PostRequest);

        /// <summary>
        /// 修改笔记服务节点
        /// </summary>
        private readonly MessageReceivingEndpoint _updateNoteEndPoint =
        new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/note/update.json", HttpDeliveryMethods.PostRequest | HttpDeliveryMethods.AuthorizationHeaderRequest);

        
        /// <summary>
        /// 移动笔记服务节点
        /// </summary>
        private readonly MessageReceivingEndpoint _moveNoteEndPoint =
        new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/note/move.json", HttpDeliveryMethods.PostRequest);
        
        /// <summary>
        /// 删除笔记服务节点
        /// </summary>
        private readonly MessageReceivingEndpoint _deleteNoteEndPoint =
        new MessageReceivingEndpoint(YDAuthBaseInfo.BaseUrl + "/yws/open/note/delete.json", HttpDeliveryMethods.PostRequest);

        /// <summary>
        /// 创建笔记 返回note的对象 还是传入参数的那个对象
        /// </summary>
        /// <returns></returns>
        public YDNote CreateNote(YDNote note)
        {
            var source = MultipartPostPart.CreateFormPart("source", note.source);
            var author = MultipartPostPart.CreateFormPart("author", note.author);
            var title = MultipartPostPart.CreateFormPart("title", note.title);
            var noteBook = MultipartPostPart.CreateFormPart("notebook", note.notebook);
            var content = MultipartPostPart.CreateFormPart("content", note.content);
            var multiparts = new List<MultipartPostPart>()
                                 {
                                     source,author,title,content,noteBook
                                 };
            var request = Consumer.PrepareAuthorizedRequest(_createNoteEndPoint, this.AccessToken, multiparts);
            var response = Consumer.Channel.WebRequestHandler.GetResponse(request);
            string body = response.GetResponseReader().ReadToEnd();
            var newNote = JsonConvert.DeserializeObject<YDNote>(body);
            note.path = newNote.path;
            note = this.GetNote(newNote.path);
            return note;
        }

        /// <summary>
        /// 获取笔记
        /// </summary>
        /// <param name="bookPath"></param>
        /// <returns></returns>
        public YDNote GetNote(string bookPath)
        {
            var extraData = new Dictionary<string, string>()
                                {
                                    { "path", bookPath }
                                };
            var request = Consumer.PrepareAuthorizedRequest(_getNoteEndPoint, this.AccessToken, extraData);
            var response = Consumer.Channel.WebRequestHandler.GetResponse(request);
            string body = response.GetResponseReader().ReadToEnd();
            var newNote = JsonConvert.DeserializeObject<YDNote>(body, new YDDateTimeConverter4s());

            return newNote;
        }

        /// <summary>
        /// 修改笔记
        /// </summary>
        /// <param name="note"></param>
        public void UpdateNote(YDNote note)
        {
            var source = MultipartPostPart.CreateFormPart("source", note.source);
            var author = MultipartPostPart.CreateFormPart("author", note.author);
            var title = MultipartPostPart.CreateFormPart("title", note.title);
            var path = MultipartPostPart.CreateFormPart("path", note.path);
            var content = MultipartPostPart.CreateFormPart("content", note.content);
            var multiparts = new List<MultipartPostPart>()
                                 {
                                     source,author,title,content,path
                                 };
            var request = Consumer.PrepareAuthorizedRequest(_updateNoteEndPoint, this.AccessToken, multiparts);
            try
            {
                Consumer.Channel.WebRequestHandler.GetResponse(request);
            }
            catch 
            {
                throw new Exception("UPDATE NOTE FAILED");
            }
        }
        /// <summary>
        /// 移动笔记
        /// </summary>
        /// <param name="souceNote">笔记</param>
        /// <param name="target">目标笔记本</param>
        /// <returns>返回原来那个笔记的对象但是path替换为move过后的</returns>
        public YDNote MoveNote(YDNote souceNote, YDNoteBook target)
        {
            var extraData = new Dictionary<string, string>()
                                {
                                    { "notebook", target.path },
                                    {"path",souceNote.path}
                                };
            var request = Consumer.PrepareAuthorizedRequest(_moveNoteEndPoint, this.AccessToken, extraData);
            var response = Consumer.Channel.WebRequestHandler.GetResponse(request);
            string body = response.GetResponseReader().ReadToEnd();
            var newNote = JsonConvert.DeserializeObject<YDNote>(body);
            souceNote = this.GetNote(newNote.path);

            return souceNote;
        }
        /// <summary>
        /// 移动笔记
        /// </summary>
        /// <param name="notePath">笔记的路径</param>
        /// <param name="notebook">笔记本路径</param>
        /// <returns>返回移动后的笔记</returns>
        public YDNote MoveNote(string notePath, string notebook)
        {
            var extraData = new Dictionary<string, string>()
                                {
                                    { "notebook",notebook },
                                    {"path",notePath}
                                };
            var request = Consumer.PrepareAuthorizedRequest(_moveNoteEndPoint, this.AccessToken, extraData);
            var response = Consumer.Channel.WebRequestHandler.GetResponse(request);
            string body = response.GetResponseReader().ReadToEnd();
            var newNote = JsonConvert.DeserializeObject<YDNote>(body);
            newNote = this.GetNote(newNote.path);

            return newNote;
        }

        /// <summary>
        /// 删除笔记
        /// </summary>
        /// <param name="path">笔记路径</param>
        public void DeleteNote(string path)
        {
            var extraData = new Dictionary<string, string>()
                                {
                                    {"path",path}
                                };
            var request = Consumer.PrepareAuthorizedRequest(_deleteNoteEndPoint, this.AccessToken, extraData);
            try
            {
                Consumer.Channel.WebRequestHandler.GetResponse(request);
            }
            catch
            {

                throw new Exception("DELETE NOTE FAILED");
            }
         
        }

        /// <summary>
        /// 删除笔记
        /// </summary>
        /// <param name="note">笔记</param>
        public void DeleteNote(YDNote note)
        {
            var extraData = new Dictionary<string, string>()
                                {
                                    {"path",note.path}
                                };
            var request = Consumer.PrepareAuthorizedRequest(_deleteNoteEndPoint, this.AccessToken, extraData);
            try
            {
                Consumer.Channel.WebRequestHandler.GetResponse(request);
            }
            catch
            {

                throw new Exception("DELETE NOTE FAILED");
            }

        }
    }
}
