using System;
using Xunit;
using memo;
using System.Collections.Generic;
using System.IO;

namespace UnitTests
{
    public class TaskWriterTest
    {
        private List<Task> list = new List<Task>();
        string filePath = Config.FilePath;

        public TaskWriterTest()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            list.Add(new Task{Name = "name1", GeneratedTime=DateTime.Now, State=TaskState.Waiting});
            list.Add(new Task{Name="name2", GeneratedTime=DateTime.Now, StartTime=DateTime.Now, State=TaskState.Started});
            list.Add(new Task{Name="name3", GeneratedTime=DateTime.Now, StartTime=DateTime.Now, State=TaskState.Halted});
            list.Add(new Task { Name = "name4", GeneratedTime = DateTime.Now, StartTime = DateTime.Now, CompleteTime = DateTime.Now, State = TaskState.Comleted });
        }

        /// <summary>
        /// listのタスクが全て指定したファイルに出力されているかのテスト
        /// ファイルが存在しない場合のテスト
        /// </summary>
        [Fact(DisplayName = "ファイルが存在しない場合にWriteがファイルを生成することのテスト")]
		public void WriteMethodCreatesFileTest()
		{
            TaskWriter.Write(list);

            //Assertion
            Assert.True(File.Exists(filePath));
		}

        [Fact(DisplayName = "Writeメソッドがlistのアイテムを全てファイルに書き込むことのテスト")]
        public void WriteMethodWriteAllItemsInListTest()
        {
			TaskWriter.Write(list);

			//Assertion
            Assert.NotEqual(0, checkFileLineNumber());
        }

		private int checkFileLineNumber()
		{
			int count = 0;
			using (StreamReader sr = new StreamReader(filePath))
			{
				while (sr.ReadLine() != null)
				{
					count++;
				}
			}
			return count;
		}

	}
}
