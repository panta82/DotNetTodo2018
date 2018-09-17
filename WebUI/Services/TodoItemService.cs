using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Data.Migrations;
using WebUI.Models;

namespace WebUI.Services {
	public class TodoItemService : ITodoItemService {
		private readonly ApplicationDbContext _context;

		public TodoItemService(ApplicationDbContext context) {
			_context = context;
		}

		public async Task<TodoItem[]> GetIncompleteItemsAsync() {
			var items = await _context.Items.Where(x => !x.IsDone).ToArrayAsync();
			return items;
		}

		public async Task<bool> AddItemAsync(TodoItem newItem) {
			newItem.Id = Guid.NewGuid();
			newItem.IsDone = false;
			newItem.DueAt = DateTimeOffset.Now.AddDays(3);

			_context.Items.Add(newItem);

			var saveResult = await _context.SaveChangesAsync();

			return saveResult == 1;
		}
	}
}