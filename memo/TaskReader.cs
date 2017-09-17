using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using memo.Cache;

namespace memo
{
    /// <summary>
    /// ファイルからタスクを読み込んでキャッシュに保存する
    /// </summary>
    public static class TaskReader
    {
        private static string FilePath = Config.FilePath;

        public static void Read()
        {
            List<Project> projects = new List<Project>();
            List<Task> tasks = new List<Task>();
            string line;
            try{
                using(var reader = new StreamReader(FilePath))
                {
                    while((line = reader.ReadLine()) != null)
                    {
                        Task task = (Task)JsonConvert.DeserializeObject(line);
                        tasks.Add(task);
                    }
                }

                Project project = new Project();
                project.TaskList = tasks;
                projects.Add(project);
                ProjectCache.Cache = projects;
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }
    }
}
