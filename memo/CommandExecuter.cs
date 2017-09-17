using System;
using System.Collections.Generic;
using System.Linq;
using memo.Cache;

namespace memo
{
    public static class CommandExecuter
    {
        /// <summary>
        /// タスクを名前で検索する
        /// </summary>
        public static List<Task> FindTaskByName(List<Task> list, string name)
		{
            return list.Where(e => e.Name == name).ToList();
        }

        /// <summary>
        /// 日付でタスクを検索する
        /// </summary>
        /// <returns><c>true</c>, if task by gen date was found, <c>false</c> otherwise.</returns>
        /// <param name="name">Name.</param>
        public static bool FindTaskDate(List<Task> list, DateTime key)
        {
            return false;
        }

        public static Task GenerateTask(string name, DateTime generateDateTime){
            return new Task(0, name);
        }

        /// <summary>
        /// タスクを開始する
        /// </summary>
        /// <param name="name">Name.</param>
        public static void StartTask(List<Task> list, string name, DateTime startDateTime)
        {
            //タスクないんなら終わり
            if(FindTaskByName(list, name).Count()==0)
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

            task.StartTime = startDateTime;
            task.State = TaskState.Started;
        }

        /// <summary>
        /// タスクを停止する
        /// WorkingTimeの更新も行う
        /// </summary>
        /// <param name="name">Name.</param>
        public static void HaltTask(List<Task> list, string name, DateTime haltDateTime)
        {
			//タスクないんなら終わり
            if (FindTaskByName(list, name).Count()==0)
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

			//停止されてた場合
			if (task.RestartTime.HasValue)
			{
                task.WorkingTime += haltDateTime - (DateTime)task.RestartTime;
			}
			else
			{
                task.WorkingTime += haltDateTime - (DateTime)task.StartTime;
			}
			task.State = TaskState.Halted;
		}

        /// <summary>
        /// タスクを再開する。
        /// </summary>
        /// <param name="name">Name.</param>
        public static void RestartTask(List<Task> list, string name, DateTime restartDateTime)
        {
            if(FindTaskByName(list, name).Count() == 0)
			{
				Console.WriteLine("指定したタスク「" + name + "」はありません。");
				return;
            }

			Task task = list.Find(e => e.Name == name);

			if (task.State != TaskState.Halted || task.State != TaskState.Comleted)
			{
				Console.WriteLine("タスクが停止状態じゃないので再開できません。");
                Console.WriteLine("今の状態：" + task.State);
				return;
			}

            task.RestartTime = restartDateTime;
        }

		/// <summary>
		/// タスクを完了する
		/// 開始状態でない または 再開状態でないなら完了にできない
		/// </summary>
        public static void CompleteTask(List<Task> list, string name, DateTime complateDateTime)
        {
            if (FindTaskByName(list, name).Count() == 0)
			{
				Console.WriteLine("指定したタスク「" + name + "」はありません。");
				return;
			}

			Task task = list.Find(e => e.Name == name);

            if (task.State != TaskState.Started || task.State != TaskState.Restarted)
			{
				Console.WriteLine("タスクが停止状態じゃないので再開できません。");
				Console.WriteLine("今の状態：" + task.State);
				return;
			}
            task.CompleteTime = complateDateTime;
            task.State = TaskState.Comleted;
		}

        /// <summary>
        /// タスクを削除する。
        /// 削除済みのタスクでない限り、状態による制限は設けない。
        /// </summary>
        /// <param name="list">List.</param>
        /// <param name="name">Name.</param>
        public static void DeleteTask(List<Task> list, string name)
        {
			if (FindTaskByName(list, name).Count() == 0)
			{
				Console.WriteLine("指定したタスク「" + name + "」はありません。");
				return;
			}

			Task task = list.Find(e => e.Name == name);

            if (task.State == TaskState.Deleted)
			{
				Console.WriteLine("削除済みのタスクです");
				return;
			}

            task.State = TaskState.Deleted;
		}

        /// <summary>
        /// タスクにメモを追加する
        /// </summary>
        /// <param name="list">List.</param>
        /// <param name="name">Name.</param>
        public static void AddMemo(List<Task>list, string name){
            
        }
    }
}
