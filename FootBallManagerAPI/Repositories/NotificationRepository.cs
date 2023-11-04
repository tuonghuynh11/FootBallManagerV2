using FootBallManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootBallManagerAPI.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly FootBallManagerV2Context _context;

        public NotificationRepository(FootBallManagerV2Context context) {
            _context = context;
        }
        public async Task<Notification> addNotificationAsync(Notification notification)
        {
            var newNoti = notification;
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return newNoti;
        }

        public async Task deleteNotificationAsync(int id)
        {
            var deleteNoti = _context.Notifications!.FirstOrDefault(x => x.Id == id);
            if (deleteNoti != null)
            {
                _context.Notifications!.Remove(deleteNoti);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Notification>> GetAllNotificationsAsync()
        {
            var notis = await _context.Notifications!.ToListAsync();
            return notis;
        }

        public async Task<Notification> GetNotificationAsync(int id)
        {
            var noti = await _context.Notifications!.FindAsync(id);
            return noti;
        }

        public async Task updateNotificationAsync(int id, Notification notification)
        {
            if(id == notification.Id)
            {
                var updateNoti = notification;
                _context.Notifications!.Update(updateNoti);
                await _context.SaveChangesAsync();
            }
        }
    }
}
