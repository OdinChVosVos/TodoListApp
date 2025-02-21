using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly TodoContext _context;

        public CategoryController(TodoContext context)
        {
            _context = context;
        }

        
        private string GetUserId()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    
            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }
    
            return userId;
        }

        public async Task<IActionResult> CategoryList()
        {
            var userId = GetUserId();
            
            var items = await _context.ItemCategory
                .Where(t => t.UserId == userId)
                .ToListAsync();
            return View(items);
        }
        
        
        public async Task<IActionResult> CategoryDetails(long id)
        {
            var userId = GetUserId();
            
            var item = await _context.ItemCategory
                .Where(t => t.UserId == userId)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (item == null)
                return NotFound();
            return View(item);
        }
        


        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryCreate(ItemCategory category)
        {
            var userId = GetUserId();
            category.UserId = userId;
            
            _context.ItemCategory.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("CategoryList", "Category");
        }


        public async Task<IActionResult> CategoryEdit(long id)
        {
            var userId = GetUserId();
            
            var item = await _context.ItemCategory
                .Where(c => c.UserId == userId)
                .FirstOrDefaultAsync(t => t.Id == id);
            
            if (item == null)
                return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryEdit(long id, ItemCategory category)
        {
            var userId = GetUserId();
            
            if (id != category.Id || category.UserId != userId)
                return BadRequest();

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("CategoryList");
        }

        
        public async Task<IActionResult> CategoryDelete(long id)
        {
            var userId = GetUserId();
            
            var item = await _context.ItemCategory.FindAsync(id);
            if (item == null || item.UserId != userId)
                return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var userId = GetUserId();
            
            var item = await _context.ItemCategory.FindAsync(id);
            if (item != null && item.UserId == userId)
            {
                _context.ItemCategory.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CategoryList", "Category");
        }
    }
}
