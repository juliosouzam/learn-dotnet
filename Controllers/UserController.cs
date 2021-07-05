using System.Linq;
using System.Collections.Generic;
using zoeira.Models;
using Faker;
using Microsoft.AspNetCore.Mvc;
using zoeira.Context;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace zoeira.Controllers
{
  [Authorize(Roles = "ADMIN,Normal")]
  // [Authorize(Roles = "Normal")]
  public class UserController : Controller
  {
    private UserContext ctx = new UserContext();

    public async Task<IActionResult> Index()
    {
      var users = new List<User>();

      var u = ctx.Users.OrderBy(u => u.Id).ToList();
      if (u.Count() > 0)
      {
        return View(u);
      }

      for (int i = 0; i < 100; i++)
      {
        var user = new User()
        {
          Name = Faker.Name.FullName(NameFormats.WithPrefix),
          Email = Faker.Internet.Email()
        };

        ctx.Update(user);
      }

      await ctx.SaveChangesAsync();

      return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int? id)
    {
      if (id == null) return NotFound();
      var ctx = new UserContext();

      var user = ctx.Users.FirstOrDefault(u => u.Id == id);

      if (user != null)
        return View(user);

      return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id, Name, Email")] User user)
    {
      if (id != user.Id) return NotFound();
      var ctx = new UserContext();

      if (ModelState.IsValid)
      {
        ctx.Update(user);
        await ctx.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
      }

      return View(user);
    }
  }
}
