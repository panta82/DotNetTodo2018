using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.Services;

namespace WebUI.Controllers {
	
	[Authorize]
	public class TodoController : Controller {
		private readonly ITodoItemService _todoItemService;
		private UserManager<IdentityUser> _userManager;

		public TodoController(ITodoItemService todoItemService, UserManager<IdentityUser> userManager) {
			_todoItemService = todoItemService;
			_userManager = userManager;
		}

		public async Task<IActionResult> Index() {
			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return Challenge();
			}
			
			var items = await _todoItemService.GetIncompleteItemsAsync(user);
			
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

			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return Challenge();
			}
			
			var ok = await _todoItemService.AddItemAsync(newItem, user);
			if (!ok) {
				return BadRequest("Could not add item");
			}

			return RedirectToAction("Index");
		}

		[ValidateAntiForgeryToken]
		public async Task<IActionResult> MarkDone(Guid id) {
			if (id == Guid.Empty) {
				return RedirectToAction("Index");
			}
			
			var user = await _userManager.GetUserAsync(User);
			if (user == null) {
				return Challenge();
			}

			var ok = await _todoItemService.MarkDoneAsync(id, user);
			if (!ok) {
				return BadRequest("Could not mark item done");
			}

			return RedirectToAction("Index");
		}
	}
}