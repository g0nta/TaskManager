using System;
namespace memo
{
    public class Task
    {
        public Task(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.GeneratedTime = DateTime.Now;
            this.State = TaskState.Waiting;
        }

        public Task() { }

        //今の所ファイルの行数に対応する・・・
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime GeneratedTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? CompleteTime { get; set; }
        public DateTime? RestartTime { get; set; }
        //workingTimeだけロジックいるかも
        private TimeSpan wt = TimeSpan.Zero;
        public TimeSpan? WorkingTime { get; set; }
        public string Memo { get; set; }
        public TaskState State { get; set; }
    }
}
