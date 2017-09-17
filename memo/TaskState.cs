using System;
namespace memo
{
    public enum TaskState
    {
		//タスク開始待ち
		Waiting = 0,
		//タスク開始済
		Started = 1,
		//タスク停止中
		Halted = 2,
        //タスク再開
        Restarted = 3,
		//タスク完了済み
		Comleted = 4,
        //タスク削除済み
        Deleted = 5
    }
}
