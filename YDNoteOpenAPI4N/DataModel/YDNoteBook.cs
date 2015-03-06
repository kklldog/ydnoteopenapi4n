//作者：       minjie.zhou
// 创建时间：   2012/12/4 0:14:14
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YDNoteOpenAPI4N.DataModel
{
    public class YDNoteBook
    {
        /// <summary>
        /// 笔记本的路径
        /// </summary>
        public string path { get; set; }
        /// <summary>
        ///  笔记本的名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 该笔记本中笔记的数目
        /// </summary>
        public int notes_num { get; set; }
        /// <summary>
        /// 笔记本的创建时间，单位秒
        /// </summary>
        public DateTime? create_time { get; set; }
        /// <summary>
        /// 笔记本的最后修改时间，单位秒
        /// </summary>
        public DateTime? modify_time { get; set; }
    }
}
