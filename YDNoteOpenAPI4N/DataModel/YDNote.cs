//作者：       minjie.zhou
// 创建时间：   2012/12/4 0:25:04
using System;

namespace YDNoteOpenAPI4N.DataModel
{
    public class YDNote
    {
        /// <summary>
        /// 笔记来源URL
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// 笔记路径
        /// </summary>
        public string notebook { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public long size { get; set; }

        /// <summary>
        ///创建时间 
        /// </summary>
        public DateTime? create_time { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? modify_time { get; set; }
    }
}
