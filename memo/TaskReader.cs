using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using memo.Cache;

namespace memo
{
    /// <summary>
    /// ファイルからタスクを読み込んでキャッシュに保存する
    /// 将来的にはprojectも読み込むようにする
    /// </summary>
    public static class TaskReader
    {
        private static string filePath = Config.FilePath;

        public static List<Project> Read()
        {
            List<Project> projects = new List<Project>();
            List<Task> tasks = new List<Task>();
            string json;

            try
            {
                using(var reader = new StreamReader(filePath))
				{
					json = reader.ReadToEnd();
                }

                tasks = JsonConvert.DeserializeObject<List<Task>>(json);

                Project project = new Project();
                project.TaskList = tasks == null ? new List<Task>() : tasks;
                projects.Add(project);

                return projects;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
