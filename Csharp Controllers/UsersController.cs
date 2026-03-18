//using Afri.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using System.Security.Claims;

//namespace Afri.Controllers
//{
//    public class UsersController : Controller
//    {
//        private readonly AFRILEARNContext _context;

//        public UsersController(AFRILEARNContext context)
//        {
//            _context = context;
//        }

//        // GET: Users
//        public async Task<IActionResult> Index()
//        {
//            var aFRILEARNContext = _context.Users
//                .Include(u => u.GradeLevel)
//                .Include(u => u.Institution)
//                .Include(u => u.PreferredLanguage)
//                .Include(u => u.Role);
//            return View(await aFRILEARNContext.ToListAsync());
//        }

//        // GET: Users/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null) return NotFound();

//            var user = await _context.Users
//                .Include(u => u.GradeLevel)
//                .Include(u => u.Institution)
//                .Include(u => u.PreferredLanguage)
//                .Include(u => u.Role)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (user == null) return NotFound();

//            return View(user);
//        }

//        // GET: Users/Create
//        public IActionResult Create()
//        {
//            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name");
//            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "Id", "Name");
//            ViewData["PreferredLanguageId"] = new SelectList(_context.Languages, "Id", "Name");
//            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
//            return View();
//        }

//        // POST: Users/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,FullName,Gender,DateOfBirth,Email,PhoneNumber,PasswordHash,RoleId,InstitutionId,PreferredLanguageId,GradeLevelId,ProfilePhotoUrl,AccountStatus,CreatedDate,ModifiedDate")] User user)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(user);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name", user.GradeLevelId);
//            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "Id", "Name", user.InstitutionId);
//            ViewData["PreferredLanguageId"] = new SelectList(_context.Languages, "Id", "Name", user.PreferredLanguageId);
//            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
//            return View(user);
//        }

//        // GET: Users/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null) return NotFound();

//            var user = await _context.Users.FindAsync(id);
//            if (user == null) return NotFound();

//            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name", user.GradeLevelId);
//            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "Id", "Name", user.InstitutionId);
//            ViewData["PreferredLanguageId"] = new SelectList(_context.Languages, "Id", "Name", user.PreferredLanguageId);
//            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
//            return View(user);
//        }

//        // POST: Users/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Gender,DateOfBirth,Email,PhoneNumber,PasswordHash,RoleId,InstitutionId,PreferredLanguageId,GradeLevelId,ProfilePhotoUrl,AccountStatus,CreatedDate,ModifiedDate")] User user)
//        {
//            if (id != user.Id) return NotFound();

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(user);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!UserExists(user.Id)) return NotFound();
//                    else throw;
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["GradeLevelId"] = new SelectList(_context.GradeLevels, "Id", "Name", user.GradeLevelId);
//            ViewData["InstitutionId"] = new SelectList(_context.Institutions, "Id", "Name", user.InstitutionId);
//            ViewData["PreferredLanguageId"] = new SelectList(_context.Languages, "Id", "Name", user.PreferredLanguageId);
//            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name", user.RoleId);
//            return View(user);
//        }

//        // GET: Users/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null) return NotFound();

//            var user = await _context.Users
//                .Include(u => u.GradeLevel)
//                .Include(u => u.Institution)
//                .Include(u => u.PreferredLanguage)
//                .Include(u => u.Role)
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (user == null) return NotFound();

//            return View(user);
//        }

//        // POST: Users/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var user = await _context.Users.FindAsync(id);
//            if (user != null)
//            {
//                _context.Users.Remove(user);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToAction(nameof(Index));
//        }

//        private bool UserExists(int id)
//        {
//            return _context.Users.Any(e => e.Id == id);
//        }

//        public IActionResult Signup() => View();

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Signup(User user, string password)
//        {
//            // Basic validation
//            if (string.IsNullOrWhiteSpace(password))
//            {
//                ModelState.AddModelError("Password", "Password is required.");
//            }

//            // Prevent duplicate emails
//            if (!string.IsNullOrWhiteSpace(user.Email) && _context.Users.Any(u => u.Email == user.Email))
//            {
//                ModelState.AddModelError("Email", "Email is already registered.");
//            }

//            // 1. Handle Password Hashing (SHA256)
//            if (ModelState.IsValid)
//            {
//                using (SHA256 sha256 = SHA256.Create())
//                {
//                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//                    user.PasswordHash = Convert.ToBase64String(bytes);
//                }

//                // 2. Set Automated Fields
//                user.CreatedDate = DateTime.Now;
//                user.AccountStatus = "Active";

//                // Lookup Student role by name (no magic numbers)
//                var studentRole = _context.Roles.FirstOrDefault(r => r.Name.Equals("Student", StringComparison.OrdinalIgnoreCase));
//                user.RoleId = studentRole?.Id ?? 1;

//                // 3. CLEANUP VALIDATION
//                ModelState.Remove("Role");
//                ModelState.Remove("GradeLevel");
//                ModelState.Remove("Institution");
//                ModelState.Remove("PreferredLanguage");
//                ModelState.Remove("PasswordHash");

//                try
//                {
//                    _context.Add(user);
//                    await _context.SaveChangesAsync();
//                    return RedirectToAction(nameof(Signin));
//                }
//                catch (Exception ex)
//                {
//                    ViewBag.Error = "Database error: " + ex.Message;
//                }
//            }

//            return View(user);
//        }

//        // GET: Signin
//        public IActionResult Signin() => View();

//        // POST: Signin -> authenticate and issue cookie with role claim
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Signin(string email, string password)
//        {
//            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
//            {
//                ViewBag.Error = "Email and password are required.";
//                return View();
//            }

//            // Hash input (keep your SHA256 approach)
//            string hashedInput;
//            using (SHA256 sha256 = SHA256.Create())
//            {
//                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//                hashedInput = Convert.ToBase64String(bytes);
//            }

//            var user = _context.Users.Include(u => u.Role)
//                        .FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedInput);

//            if (user == null)
//            {
//                ViewBag.Error = "Invalid email or password.";
//                return View();
//            }

//            // Create claims from user (include role)
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.FullName ?? user.Email),
//                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
//                new Claim(ClaimTypes.Role, user.Role?.Name ?? "Student")
//            };

//            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
//            var principal = new ClaimsPrincipal(identity);

//            // Sign in (issue cookie)
//            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
//                new AuthenticationProperties { IsPersistent = true });

//            // Keep session values for compatibility with CurrentUser / legacy code
//            HttpContext.Session.SetInt32("UserId", user.Id);
//            HttpContext.Session.SetString("UserName", user.FullName ?? user.Email);
//            HttpContext.Session.SetString("UserRole", user.Role?.Name ?? "Student");

//            return RedirectToAction("Index", "Home");
//        }

//        public IActionResult Logout()
//        {
//            // Clear session and sign out cookie
//            HttpContext.Session.Clear();
//            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
//            return RedirectToAction(nameof(Signin));
//        }
//    }
//}