using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootBallManagerAPI.Entities;
using FootBallManagerAPI.Repositories;

namespace FootBallManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationRepository _notiRepo;

        public NotificationsController(INotificationRepository notificationRepository)
        {
            _notiRepo = notificationRepository;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications()
        {
            try
            {
                return Ok(await _notiRepo.GetAllNotificationsAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            try
            {

                var itemtype = await _notiRepo.GetNotificationAsync(id);
                return itemtype == null ? NotFound() : Ok(itemtype);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Notifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotification(int id, Notification notification)
        {
            try
            {

                if (id != notification.Id)
                {
                    return NotFound();
                }
                await _notiRepo.updateNotificationAsync(id, notification);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: api/Notifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Notification>> PostNotification(Notification notification)
        {
            try
            {
                var newNoti = await _notiRepo.addNotificationAsync(notification);
                return newNoti == null ? NotFound() : Ok(newNoti);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            try
            {

                await _notiRepo.deleteNotificationAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
