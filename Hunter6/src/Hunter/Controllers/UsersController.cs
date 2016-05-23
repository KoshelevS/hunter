using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore .Mvc;

using Hunter.Security.Model;
using Hunter6.ViewModels.Users;
using Microsoft.EntityFrameworkCore;

namespace Hunter.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.Select(u => ToUserViewModel(u)).ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = await ToUserViewModel(_userManager.FindByIdAsync(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = await ToUserViewModel(_userManager.FindByIdAsync(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userViewModel.Id);
                user.UserName = userViewModel.Name;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }

        // GET: Users/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserViewModel userViewModel = await ToUserViewModel(_userManager.FindByIdAsync(id));
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
            return RedirectToAction("Index");
        }

        private static async Task<UserViewModel> ToUserViewModel(Task<ApplicationUser> task)
        {
            var user = await task;
            return ToUserViewModel(user);
        }

        private static UserViewModel ToUserViewModel(ApplicationUser user)
        {
            return new UserViewModel { Id = user.Id, Name = user.UserName, Email = user.Email };
        }
    }
}
