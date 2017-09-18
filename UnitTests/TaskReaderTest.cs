using System;
using Xunit;
using memo;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace UnitTests
{
    public class TaskReaderTest
	{
		private string filePath = Config.FilePath;
		private List<Task> list = new List<Task>();

        public TaskReaderTest()
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			list.Add(new Task { Name = "name1", GeneratedTime = DateTime.Now, State = TaskState.Waiting });
			list.Add(new Task { Name = "name2", GeneratedTime = DateTime.Now, StartTime = DateTime.Now, State = TaskState.Started });
			list.Add(new Task { Name = "name3", GeneratedTime = DateTime.Now, StartTime = DateTime.Now, State = TaskState.Halted });
			list.Add(new Task { Name = "name4", GeneratedTime = DateTime.Now, StartTime = DateTime.Now, CompleteTime = DateTime.Now, State = TaskState.Comleted });
		
            using(StreamWriter w = new StreamWriter(filePath)){
                string jsonStr = JsonConvert.SerializeObject(list, Formatting.Indented);
                w.Write(jsonStr);
            }
        }

        [Fact]
        public void DeserializeTest(){
            List<Project> projectList = TaskReader.Read();
            int taskCount = projectList[0].TaskList.Count;
            Assert.Equal(4, taskCount);
        }
    }
}
