namespace TaskMgmt.Services.Interfaces
{

    public interface INotificationService
    {
        // void Notify(int recipientId, string subject, string message);
        public Task NotifyAsync(string recipientId, string subject, string message);
        public Task BulkNotifyAsync(IEnumerable<string> recipientIds, string subject, string message);
    }
}
