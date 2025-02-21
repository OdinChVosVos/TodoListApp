using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> TodoList()
        {
            var userId = GetUserId();

            var todos = await _context.TodoItems
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .ToListAsync();

            return View(todos);
        }


        
        
        public async Task<IActionResult> TodoDetails(long id)
        {
            var userId = GetUserId();
            
            var item = await _context.TodoItems
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (item == null)
                return NotFound();
            return View(item);
        }


        public async Task<IActionResult> TodoCreate()
        {
            var userId = GetUserId();
            
            ViewBag.Categories = new SelectList(await _context.ItemCategory
                .Where(c => c.UserId == userId)
                .ToListAsync(), "Id", "Title");
            return View();
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


        [HttpPost]
        public async Task<IActionResult> TodoCreate(TodoItem todoItem)
        {
            var userId = GetUserId();

            
            if (todoItem.CategoryId.HasValue)
            {
                var category = await _context.ItemCategory
                    .FirstOrDefaultAsync(c => c.Id == todoItem.CategoryId && c.UserId == userId);

                todoItem.Category = category;
            }
            
            todoItem.UserId = userId;

            
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
        
            return RedirectToAction("TodoList");
        }




        public async Task<IActionResult> TodoEdit(long id)
        {
            var userId = GetUserId();
            
            var todoItem = await _context.TodoItems
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (todoItem == null || todoItem.UserId != userId)
            {
                return NotFound();
            }

      
            ViewBag.Categories = new SelectList(await _context.ItemCategory
                .Where(c => c.UserId == userId)
                .ToListAsync(), "Id", "Title", todoItem.CategoryId);
            
            return View(todoItem);
        }

        [HttpPost]
        public async Task<IActionResult> TodoEdit(long id, TodoItem todoItem)
        {
            var userId = GetUserId();
            
            if (id != todoItem.Id || todoItem.UserId != userId)
                return BadRequest();

            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("TodoList");
        }

        
        public async Task<IActionResult> TodoDelete(long id)
        {
            var userId = GetUserId();
            
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null || item.UserId != userId)
                return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var userId = GetUserId();
            
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null && item.UserId == userId)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("TodoList", "Todo");
        }
    }
}
