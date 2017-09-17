using System;
namespace memo.Mapper
{
    public static class TaskMapper
    {
        public static Task MapTask(
            int id,
            string name,
            DateTime generatedTime,
            DateTime? startTime,
            DateTime? completeTime,
            string memo,
            TaskState state)
        {
            return new Task()
            {
                Id = id,
                Name = name,
                GeneratedTime = generatedTime,
                StartTime = startTime,
                CompleteTime = completeTime,
                Memo = memo,
                State = state
            };
        }
    }
}
