using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Services {
	public interface ITodoItemService {
		Task<TodoItem[]> GetIncompleteItemsAsync();
		Task<bool> AddItemAsync(TodoItem newItem);
	}
}