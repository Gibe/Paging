namespace Gibe.Pager.Models
{
	public class PaginationItem
	{
		public string Url { get; set; }
		public string Text { get; set; }
		public bool IsActive { get; set; }
		public bool IsEllipsis { get; set; }
	}
}