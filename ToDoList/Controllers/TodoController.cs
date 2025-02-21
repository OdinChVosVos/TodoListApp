using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> TodoList()
        {
            var items = await _context.TodoItems.ToListAsync();
            return View(items);
        }
        
        
        public async Task<IActionResult> TodoDetails(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null)
                return NotFound();
            return View(item);
        }


        public IActionResult TodoCreate()
        {
            var categories = _context.ItemCategory.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TodoCreate(TodoItem todoItem)
        {
            
            if (ModelState.IsValid)
            {
                var category = await _context.ItemCategory.FindAsync(todoItem.CategoryId);
        
                if (category != null)
                {
                    todoItem.Category = category;
                }
                
                _context.TodoItems.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("TodoList", "Todo");
            }
            
            ViewBag.Categories = new SelectList(_context.ItemCategory.ToList(), "Id", "Title");
            return View(todoItem);
        }


        public async Task<IActionResult> TodoEdit(long id)
        {
            var todoItem = _context.TodoItems.Include(t => t.Category)
                .FirstOrDefault(t => t.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

      
            var categories = _context.ItemCategory.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Title", todoItem.CategoryId);
            
            return View(todoItem);
        }

        [HttpPost]
        public async Task<IActionResult> TodoEdit(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
                return BadRequest();

            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToAction("TodoList");
        }

        // Show delete confirmation page
        public async Task<IActionResult> TodoDelete(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item == null)
                return NotFound();
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var item = await _context.TodoItems.FindAsync(id);
            if (item != null)
            {
                _context.TodoItems.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("TodoList", "Todo");
        }
    }
}
