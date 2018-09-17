using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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

		public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user) {
			var items = await _context.Items
				.Where(x => !x.IsDone && x.UserId == user.Id)
				.ToArrayAsync();
			return items;
		}

		public async Task<bool> AddItemAsync(TodoItem newItem, IdentityUser user) {
			newItem.Id = Guid.NewGuid();
			newItem.IsDone = false;
			newItem.DueAt = DateTimeOffset.Now.AddDays(3);
			newItem.UserId = user.Id;

			_context.Items.Add(newItem);

			var saveResult = await _context.SaveChangesAsync();

			return saveResult == 1;
		}

		public async Task<bool> MarkDoneAsync(Guid id, IdentityUser user) {
			var item = await _context.Items.FindAsync(id);
			if (item == null || item.UserId != user.Id) {
				return false;
			}

			item.IsDone = true;
			return await _context.SaveChangesAsync() == 1;
		}
	}
}