using FootBallManagerAPI.Entities;

namespace FootBallManagerAPI.Repositories
{
    public interface INotificationRepository
    {
        public Task<List<Notification>> GetAllNotificationsAsync();
        public Task<Notification> GetNotificationAsync(int id);
        public Task<int> addNotificationAsync(Notification notification);
        public Task updateNotificationAsync(int id,  Notification notification);
        public Task deleteNotificationAsync(int id);
    }
}
