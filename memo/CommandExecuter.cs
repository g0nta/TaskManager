using System;
using System.Collections.Generic;
using System.Linq;
using memo.Cache;

namespace memo
{
    public static class CommandExecuter
    {
        /// <summary>
        /// タスクを名前で検索し、あればtrue, なければfalseを返す
        /// </summary>
        /// <returns><c>true</c>, if task by name was found, <c>false</c> otherwise.</returns>
        /// <param name="name">Name.</param>
        public static IEnumerable<Task> FindTaskByName(string name)
		{
			List<Task> list = ProjectCache.Cache[0].TaskList;
            return list.Where(e => e.Name == name);
        }

        /// <summary>
        /// 日付でタスクを検索する
        /// </summary>
        /// <returns><c>true</c>, if task by gen date was found, <c>false</c> otherwise.</returns>
        /// <param name="name">Name.</param>
        public static bool FindTaskDate(DateTime key)
        {
            return false;
        }

        /// <summary>
        /// タスクを開始する
        /// </summary>
        /// <param name="name">Name.</param>
        public static void StartTask(string name)
        {
            List<Task> list = ProjectCache.Cache[0].TaskList;
            //タスクないんなら終わり
            if(FindTaskByName(name).Count()==0)
            {
                Console.WriteLine("指定したタスク「" + name + "」はありません。");
                return;
            }

			Task task = list.Find(e => e.Name == name);

			//タスク開始待ちじゃないなら開始できない
			if (task.State != TaskState.Waiting)
			{
				Console.WriteLine("タスクが開始待ち状態じゃないので開始できません。");
				Console.WriteLine("今の状態：" + task.State);
				return;
			}

            task.StartTime = DateTime.Now;
            task.State = TaskState.Started;
        }

        /// <summary>
        /// タスクを停止する
        /// WorkingTimeの更新も行う
        /// </summary>
        /// <param name="name">Name.</param>
        public static void HaltTask(string name)
        {
            List<Task> list = ProjectCache.Cache[0].TaskList;

			//タスクないんなら終わり
            if (FindTaskByName(name).Count()==0)
			{
				Console.WriteLine("指定したタスク「" + name + "」はありません。");
				return;
			}

			Task task = list.Find(e => e.Name == name);

			//開始済み状態じゃないと停止できない
			if (task.State != TaskState.Started)
			{
				Console.WriteLine("タスクが開始状態じゃないので停止できません。");
                Console.WriteLine("今の状態：" + task.State);
				return;
			}

			DateTime now = DateTime.Now;
			//停止されてた場合
			if (task.RestartTime.HasValue)
			{
                task.WorkingTime += now - (DateTime)task.RestartTime;
			}
			else
			{
                task.WorkingTime += now - (DateTime)task.StartTime;
			}
			task.State = TaskState.Halted;
		}

        /// <summary>
        /// タスクを再開する。
        /// </summary>
        /// <param name="name">Name.</param>
        public static void RestartTask(string name)
        {
            List<Task> list = ProjectCache.Cache[0].TaskList;

            if(FindTaskByName(name).Count() == 0)
			{
				Console.WriteLine("指定したタスク「" + name + "」はありません。");
				return;
            }

			Task task = list.Find(e => e.Name == name);
            DateTime now = DateTime.Now;

			if (task.State != TaskState.Halted || task.State != TaskState.Comleted)
			{
				Console.WriteLine("タスクが停止状態じゃないので再開できません。");
                Console.WriteLine("今の状態：" + task.State);
				return;
			}

            task.RestartTime = now;
        }

		/// <summary>
		/// タスクを完了する
		/// 開始状態でない または 再開状態でないなら完了にできない
		/// </summary>
		public static void CompleteTask(string name)
        {
			List<Task> list = ProjectCache.Cache[0].TaskList;

            if (FindTaskByName(name).Count() == 0)
			{
				Console.WriteLine("指定したタスク「" + name + "」はありません。");
				return;
			}

			Task task = list.Find(e => e.Name == name);
			DateTime now = DateTime.Now;

            if (task.State != TaskState.Started || task.State != TaskState.Restarted)
			{
				Console.WriteLine("タスクが停止状態じゃないので再開できません。");
				Console.WriteLine("今の状態：" + task.State);
				return;
			}
            task.CompleteTime = DateTime.Now;
            task.State = TaskState.Comleted;
		}
    }
}
