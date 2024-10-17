using AdminCrud.Services;
using AdminCrud.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AdminCrud.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            try
            {
                await _authService.RegisterAsync(dto);
                return RedirectToAction(nameof(Login));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            try
            {
                await _authService.LoginAsync(dto);
                return RedirectToAction("Index", "Home"); // Redirect after successful login
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(dto); // Show login page with error message
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Logout()
        //{
        //    //HttpContext.Session.Clear();
        //    await _authService.Logout();
        //    //return RedirectToAction(nameof(Login));
        //    return RedirectToAction("Login","Auth");

        //}
        
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return RedirectToAction("Login");
        }
    }
}
