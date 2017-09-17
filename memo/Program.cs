using System;
using System.Collections.Generic;
using memo.Cache;

namespace memo
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length==0){
                Console.WriteLine("ヘルプメッセージ");
                return;
            }
            if(args.Length != 2){
                Console.WriteLine("警告メッセージ");
                return;
            }

            string cmd = args[0];
            string taskName = args[1];

            TaskReader.Read();
            List<Task> list = ProjectCache.Cache[0].TaskList;

			DateTime now = DateTime.Now;

            ExecuteCommand(list, cmd, taskName, now);

            TaskWriter.Write(list);
        }

        /// <summary>
        /// 指定されたコマンドを実行する
        /// </summary>
        /// <param name="list">List.</param>
        /// <param name="cmd">Cmd.</param>
        /// <param name="taskName">Task name.</param>
        /// <param name="now">Now.</param>
        private static void ExecuteCommand(List<Task> list, string cmd, string taskName, DateTime now)
        {
			switch (cmd)
			{
				case "new":
				case "-n":
					Task task = CommandExecuter.GenerateTask(taskName, now);
					break;
				case "start":
				case "-s":
					CommandExecuter.StartTask(list, taskName, now);
					break;
                case "halt":
                case "-h":
                    CommandExecuter.HaltTask(list, taskName, now);
                    break;
                case "restart":
                case "-r":
                    CommandExecuter.RestartTask(list, taskName, now);
                    break;
                case "complate":
                case "-c":
                    CommandExecuter.CompleteTask(list, taskName, now);
                    break;
                case "delete":
                case "-d":
                    CommandExecuter.DeleteTask(list, taskName);
                    break;
                default:
                    Console.WriteLine("定義されていないコマンドです");
                    break;
			} 
        }
    }
}
