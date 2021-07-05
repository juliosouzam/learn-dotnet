using Microsoft.AspNetCore.Mvc;

namespace zoeira.Controllers
{
  public class PostController : Controller
  {
    public string Index()
    {
      return "Hello, Boss";
    }

    public string Show(int id)
    {
      return $@"Post: {id} => \n";
    }
  }
}