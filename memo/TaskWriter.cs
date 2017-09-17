using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Generic;
using memo.Cache;

namespace memo
{
    public static class TaskWriter
	{
		private static string filePath = Config.FilePath;

        public static void Write(List<Task> list)
        {
            //ProjectCache使ってるせいでテストが辛い
            if(File.Exists(filePath)){
                File.Create(filePath);
            }
            string taskListJsonString = JsonConvert.SerializeObject(list,Formatting.Indented);
			try
			{
				using (StreamWriter writer = new StreamWriter(filePath))
				{
                    writer.WriteLine(taskListJsonString);
				}

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }

        public static void InsertTask(Task task)
        {
            string taskJsonString
                = JsonConvert.SerializeObject(task);

        }
        public static bool UpdateTask(Task task){
            return true;
        }
    }
}
