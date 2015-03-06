//作者：       minjie.zhou
// 创建时间：   2012/12/3 23:16:38
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YDNoteOpenAPI4N.DataModel
{

    /// <summary>
    /// 有道笔记 USER 模型
    /// </summary>
    public class YDUser
    {
        /// <summary>
        /// user id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 用户Name
        /// </summary>
        public string user { get; set; }
        /// <summary>
        /// 用户总的空间大小，单位字节
        /// </summary>
        public long total_size { get; set; }
        /// <summary>
        /// 用户已经使用了的空间大小，单位字节
        /// </summary>
        public long used_size { get; set; }

        /// <summary>
        /// 用户注册时间，单位毫秒
        /// </summary>
        public DateTime? register_time { get; set; }

        /// <summary>
        ///  用户最后登录时间，单位毫秒
        /// </summary>
        public DateTime? last_login_time { get; set; }
        /// <summary>
        ///  用户最后修改时间，单位毫秒
        /// </summary>
        public DateTime? last_modify_time { get; set; }
        /// <summary>
        /// 该应用的默认笔记本
        /// </summary>
        public string default_notebook { get; set; }
    }
}
