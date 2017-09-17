using System;
using Xunit;
using memo;
using System.Collections.Generic;
using System.IO;
namespace UnitTests
{
    public class CommandExecuterTest
    {
        private List<Task> list4FindTaskByNameTest = new List<Task>();
        private List<Task> list4StartTask = new List<Task>();

        public CommandExecuterTest()
        {
            //FindTaskByNameのテスト用
            list4FindTaskByNameTest.Add(new Task { Name = "name1", GeneratedTime = DateTime.Now, State = TaskState.Waiting });

            //StartTaskのテスト用
            list4StartTask.Add(new Task { Name = "name1", GeneratedTime = DateTime.Now, State = TaskState.Waiting });
            list4StartTask.Add(new Task { Name = "name2", GeneratedTime = DateTime.Now, State = TaskState.Started });
            list4StartTask.Add(new Task { Name = "name3", GeneratedTime = DateTime.Now, State = TaskState.Halted });
            list4StartTask.Add(new Task { Name = "name4", GeneratedTime = DateTime.Now, State = TaskState.Restarted });
            list4StartTask.Add(new Task { Name = "name5", GeneratedTime = DateTime.Now, State = TaskState.Comleted });
            list4StartTask.Add(new Task { Name = "name6", GeneratedTime = DateTime.Now, State = TaskState.Deleted });
        }

        [Fact]
        public void FindTaskByNameMethodFindsTaskTest()
        {
            List<Task> list = CommandExecuter.FindTaskByName(list4FindTaskByNameTest, "name1");
            Assert.Equal(list.Count, 1);
        }

        [Fact]
        public void FindTaskByNameMethodReturnNullWhenThereIsNoMatchedData()
        {
            List<Task> list = CommandExecuter.FindTaskByName(list4FindTaskByNameTest, "name2");
            Assert.Equal(0, list.Count);
        }

        #region StartTaskのテスト
        [Fact]
        public void StartTaskTest1()
        {
            DateTime startDateTime = DateTime.Now;
            CommandExecuter.StartTask(list4StartTask, "name1", startDateTime);
            Task task = list4StartTask.Find(e => e.Name == "name1");
            Assert.Equal(task.State, TaskState.Started);
            Assert.Equal(startDateTime, task.StartTime);
        }

        [Fact]
        public void StartTaskTest2()
        {
            DateTime startDateTime = DateTime.Now;
            CommandExecuter.StartTask(list4StartTask, "name2", startDateTime);

            Task startedTask = list4StartTask.Find(e => e.Name == "name2");
            Task haltedTask = list4StartTask.Find(e => e.Name == "name3");
            Task restaredTask = list4StartTask.Find(e => e.Name == "name4");
            Task completedTask = list4StartTask.Find(e => e.Name == "name5");
            Task deletedTask = list4StartTask.Find(e => e.Name == "name6");

            Assert.Equal(TaskState.Started, startedTask.State);
            Assert.Null(startedTask.StartTime);

            Assert.Equal(TaskState.Halted, haltedTask.State);
            Assert.Null(haltedTask.StartTime);

            Assert.Equal(TaskState.Restarted, restaredTask.State);
            Assert.Null(restaredTask.StartTime);

            Assert.Equal(TaskState.Comleted, completedTask.State);
            Assert.Null(completedTask.StartTime);

            Assert.Equal(TaskState.Deleted, deletedTask.State);
            Assert.Null(deletedTask.StartTime);
        }
        #endregion
    }
}
