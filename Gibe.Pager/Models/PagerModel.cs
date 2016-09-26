using System.Collections.Generic;

namespace Gibe.Pager.Models
{
	public class PagerModel
	{
		public IList<PaginationItem> PaginationItems { get; set; }
		public int TotalItemCount { get; set; }
		public int TotalPages { get; set; }
		public int CurrentPage { get; set; }
		public int ItemsPerPage { get; set; }
		public bool ShowFirstButton { get; set; }
		public bool ShowLastButton { get; set; }
		public bool ShowPreviousButton { get; set; }
		public bool ShowNextButton { get; set; }
		public string PageQueryStringKey { get; set; }
		public PaginationItem FirstButton { get; set; }
		public PaginationItem LastButton { get; set; }
		public PaginationItem PreviousButton { get; set; }
		public PaginationItem NextButton { get; set; }
	}
}