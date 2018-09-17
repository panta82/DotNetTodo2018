using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Controllers {
	public class TodoController : Controller {
		private readonly ITodoItemService _todoItemService;

		public TodoController(ITodoItemService todoItemService) {
			_todoItemService = todoItemService;
		}

		public async Task<IActionResult> Index() {
			var items = await _todoItemService.GetIncompleteItemsAsync();
			
			var model = new TodoViewModel {
				Items = items
			};

			return View(model);
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddItem(TodoItem newItem) {
			if (!ModelState.IsValid) {
				return RedirectToAction("Index");
			}

			var ok = await _todoItemService.AddItemAsync(newItem);
			if (!ok) {
				return BadRequest("Could not add item");
			}

			return RedirectToAction("Index");
		}
	}
}