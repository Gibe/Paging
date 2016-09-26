using System.Collections.Generic;
using Gibe.Pager.Models;

namespace Gibe.Pager.Interfaces
{
	public interface IPagerService
	{
		PagerModel GetPager(int totalItems, int itemsPerPage, int currentPage, string pageQueryStringKey = "page");
		IEnumerable<T> GetCurrentPageOfItems<T>(IEnumerable<T> items, int itemsPerPage, int currentPage);
		PageQueryResultModel<T> GetPageQueryResultModel<T>(IEnumerable<T> itemsForCurrentPage, int itemsPerPage, int currentPage, int totalItems);
	}
}