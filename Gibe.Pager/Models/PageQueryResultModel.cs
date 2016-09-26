using System.Collections.Generic;

namespace Gibe.Pager.Models
{
	public class PageQueryResultModel<T>
	{
		public IEnumerable<T> CurrentPageItems { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public int ItemsPerPage { get; set; }
	}
}