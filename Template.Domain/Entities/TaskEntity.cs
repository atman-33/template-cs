using Template.Domain.ValueObjects;

namespace Template.Domain.Entities
{
    public sealed class TaskEntity
    {
        public TaskEntity(int id, string content, DateTime deadline)
        {
            TaskId = new TaskId(id);
            TaskContent = new TaskContent(content);
            TaskDeadline = new TaskDeadline(deadline);
        }

        public TaskId TaskId { get; }
        public TaskContent TaskContent { get; }
        public TaskDeadline TaskDeadline { get; }
    }
}
