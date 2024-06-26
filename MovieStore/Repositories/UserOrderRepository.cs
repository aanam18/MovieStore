using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MovieStore.Repositories
{
    public class UserOrderRepository :IUserOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public UserOrderRepository(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
             IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Order>> UserOrders()
        {
            //var orders = _db.Orders
            //               .Include(x => x.OrderStatus)
            //               .Include(x => x.OrderDetail)
            //               .ThenInclude(x => x.Book)
            //               .ThenInclude(x => x.Genre).AsQueryable();
            //if (!getAll)
            //{
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged-in");
               var orders = await _db.Orders
                                        .Include(x => x.OrderStatus)
                                     .Include(x=>x.OrderDetail)
                                     
                                     .ThenInclude(x => x.Book)
                                     .ThenInclude(x=>x.Genre)
                            .Where(a => a.UserId == userId)
                .ToListAsync();
            //}
            return orders;
            //return await orders.ToListAsync();
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
}
