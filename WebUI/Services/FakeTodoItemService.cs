using System;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Services {
	public class FakeTodoItemService : ITodoItemService {
		public Task<TodoItem[]> GetIncompleteItemsAsync() {
			return Task.FromResult(new[] {
				new TodoItem {
					Title = "Todo 1",
					DueAt = DateTimeOffset.Now.AddDays(1)
				},

				new TodoItem {
					Title = "Todo 2",
					DueAt = DateTimeOffset.Now.AddDays(2)
				}
			});
		}
	}
}