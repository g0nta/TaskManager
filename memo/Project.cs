using System;
using System.Collections.Generic;
namespace memo
{
    /// <summary>
    /// 1プロジェクト1ファイルにする予定
    /// 今は仮置き。タスク数だけ持つようにする
    /// </summary>
    public class Project
    {
        public Project()
        {
            TaskCount = 0;
        }
        public int TaskCount { get; set; }
        public List<Task> TaskList { get; set; }
    }
}
