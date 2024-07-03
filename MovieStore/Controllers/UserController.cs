using Microsoft.AspNetCore.Mvc;

namespace MovieStore.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            List<UserViewModel> userList = new List<UserViewModel>();
            if (users != null)
            {

                foreach (var user in users)
                {
                    var userViewModel = new UserViewModel()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    };
                    userList.Add(userViewModel);
                }
                return View(userList);
            }
            return View(userList);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserViewModel employeeData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User()
                    {
                        FirstName = employeeData.FirstName,
                        LastName = employeeData.LastName,
                        Email = employeeData.Email
                    };
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    TempData["successMessage"] = "User Created successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Details should be filled";
                    return View();

                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(x => x.Id == Id);

                if (user != null)
                {
                    var userView = new UserViewModel()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    };
                    return View(userView);
                }
                else
                {
                    TempData["errorMessage"] = $"User  details not avaiable with this ID :{Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");

            }

        }
        [HttpPost]
        public IActionResult Edit(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new User()
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email
                    };
                    _context.Users.Update(user);
                    _context.SaveChanges();
                    TempData["successMessage"] = "User details updated successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Invalid Details";
                    return View();
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(x => x.Id == Id);

                if (user != null)
                {
                    var userView = new UserViewModel()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email
                    };
                    return View(userView);
                }
                else
                {
                    TempData["errorMessage"] = $"User details not avaiable with this ID :{Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");

            }

        }
        [HttpPost]
        public IActionResult Delete(UserViewModel model)
        {
            try
            {
                var user = _context.Users.SingleOrDefault(x => x.Id == model.Id);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Delete successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"User details not avaiable with this ID ";
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();


            }
        }
    }
}

