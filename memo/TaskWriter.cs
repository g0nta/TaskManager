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
            if(!File.Exists(filePath)){
                File.Create(filePath).Close();
            }
            string taskListJsonString = JsonConvert.SerializeObject(list,Formatting.Indented);
            //var writer = new StreamWriter(filePath);
			try
			{
                using(var writer = new StreamWriter(filePath))
				{
					writer.Write(taskListJsonString);
                }

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
        }
    }
}
