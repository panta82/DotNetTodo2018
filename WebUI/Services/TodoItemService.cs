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
	}
}