using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class CategoryController : Controller
    {
        private readonly TodoContext _context;

        public CategoryController(TodoContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> CategoryList()
        {
            var items = await _context.ItemCategory.ToListAsync();
            return View(items);
        }
        
        
        public async Task<IActionResult> CategoryDetails(long id)
        {
            var item = await _context.ItemCategory.FindAsync(id);
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
            if (ModelState.IsValid)
            {
                _context.ItemCategory.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("CategoryList", "Category");
            }
            return View(category);
        }


        public async Task<IActionResult> CategoryEdit(long id)
        {
            var item = await _context.ItemCategory.FindAsync(id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryEdit(long id, ItemCategory category)
        {
            if (id != category.Id)
                return BadRequest();

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("CategoryList");
        }

        // Show delete confirmation page
        public async Task<IActionResult> CategoryDelete(long id)
        {
            var item = await _context.ItemCategory.FindAsync(id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var item = await _context.ItemCategory.FindAsync(id);
            if (item != null)
            {
                _context.ItemCategory.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("CategoryList", "Category");
        }
    }
}
