using DBFirstDemo1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class RestaurantsController : Controller
{
    private readonly FoodieAppDbContext _db;
    public RestaurantsController(FoodieAppDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        // Read from DB using scaffolded DbSet
        var list = await _db.Restaurants
                            .AsNoTracking()
                            .OrderBy(r => r.Name)
                            .ToListAsync();

        return View(list);
    }
}