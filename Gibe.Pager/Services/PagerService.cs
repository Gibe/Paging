using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.Pager.Extensions;
using Gibe.Pager.Interfaces;
using Gibe.Pager.Models;

namespace Gibe.Pager.Services
{
	public class PagerService : IPagerService
	{
		private readonly ICurrentUrlService _currentUrlService;

		public PagerService(ICurrentUrlService currentUrlService)
		{
			_currentUrlService = currentUrlService;
		}

		public PagerModel GetPager(int totalItems, int itemsPerPage, int currentPage, string pageQueryStringKey = "page")
		{
			var totalPages = (int)Math.Ceiling(totalItems / (decimal)itemsPerPage);

			var pagerModel = new PagerModel
			{
				TotalItemCount = totalItems,
				PaginationItems = GetPaginationItemsToDisplay(totalPages, currentPage, pageQueryStringKey),
				TotalPages = totalPages,
				ItemsPerPage = itemsPerPage,
				CurrentPage = currentPage,
				ShowFirstButton = currentPage > 1,
				ShowLastButton = currentPage < totalPages,
				ShowPreviousButton = currentPage > 1,
				ShowNextButton = totalPages > 1 && currentPage != totalPages,
				FirstButton = new PaginationItem
				{
					Url = GetUrl(1, pageQueryStringKey),
					Text = "First"
				},
				LastButton = new PaginationItem
				{
					Url = GetUrl(totalPages, pageQueryStringKey),
					Text = "Last"
				},
				PreviousButton = new PaginationItem
				{
					Url = GetUrl(currentPage - 1, pageQueryStringKey),
					Text = "Previous"
				},
				NextButton = new PaginationItem
				{
					Url = GetUrl(currentPage + 1, pageQueryStringKey),
					Text = "Next"
				}
			};

			return pagerModel;
		}

		private IList<PaginationItem> GetPaginationItemsToDisplay(int totalPages, int currentPage, string pageQueryStringKey)
		{
			var paginationItems = new List<PaginationItem>();

			if (totalPages <= 10)
			{
				return GetPaginationItems(1, totalPages, currentPage, pageQueryStringKey).ToList();
			}

			if (currentPage > 3)
			{
				paginationItems.Add(new PaginationItem
				{
					Text = "1",
					Url = GetUrl(1, pageQueryStringKey)
				});
				if (currentPage > 4)
				{
					paginationItems.Add(GetPaginationItemWithEllipsis());
				}
			}

			var startPage = Math.Max(1, currentPage - 2);
			var endPage = Math.Min(totalPages, startPage + 5);

			paginationItems.AddRange(GetPaginationItems(startPage, endPage, currentPage, pageQueryStringKey));

			if (endPage < totalPages)
			{
				if (endPage < totalPages - 1)
				{
					paginationItems.Add(GetPaginationItemWithEllipsis());
				}

				paginationItems.Add(new PaginationItem
				{
					IsActive = totalPages == currentPage,
					Text = totalPages.ToString(),
					Url = GetUrl(totalPages, pageQueryStringKey)
				});
			}

			return paginationItems;
		}

		private IEnumerable<PaginationItem> GetPaginationItems(int startPage, int endPage, int currentPage, string pageQueryStringKey)
		{
			for (var i = startPage; i <= endPage; i++)
			{
				yield return new PaginationItem
				{
					IsActive = i == currentPage,
					Text = i.ToString(),
					Url = GetUrl(i, pageQueryStringKey)
				};
			}
		}

		private string GetUrl(int page, string pageQueryStringKey)
		{
			var currentUri = new UriBuilder(_currentUrlService.CurrentUrl()).Uri;
			currentUri = currentUri.ExtendQuery(new Dictionary<string, string> { { pageQueryStringKey, page.ToString() } });
			return currentUri.PathAndQuery;
		}

		private PaginationItem GetPaginationItemWithEllipsis() => new PaginationItem { Text = "...", IsEllipsis = true };

		public PageQueryResultModel<T> GetPageQueryResultModel<T>(IEnumerable<T> currentPageItems, int itemsPerPage, int currentPage, int totalItemCount)
		{
			return new PageQueryResultModel<T>
			{
				CurrentPageItems = currentPageItems,
				TotalItemCount = totalItemCount,
				TotalPages = (int)Math.Ceiling(totalItemCount / (decimal)itemsPerPage),
				ItemsPerPage = itemsPerPage,
				CurrentPage = currentPage
			};
		}

		public IEnumerable<T> GetCurrentPageOfItems<T>(IEnumerable<T> items, int itemsPerPage, int currentPage)
		{
			return items.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);
		}
	}
}