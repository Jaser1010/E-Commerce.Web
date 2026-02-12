using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Shared
{
	public class ProductQueryParams
	{
		public int? typeId { get; set; }
		public int? brandId { get; set; }
		public string? search { get; set; }
		public ProductSortingOptions Sort { get; set; }
		private int _pageIndex = 1;
		public int PageIndex
		{
			get => _pageIndex;
			set => _pageIndex = (value < 1) ? 1 : value;
		}
		private const int MaxPageSize = 10;
		private const int DefaultPageSize = 5;
		private int _pageSize = DefaultPageSize;
		public int PageSize
		{
			get => _pageSize;
			set
			{
				if (value < 1)
					_pageSize = DefaultPageSize;
				else if(value > MaxPageSize)
					_pageSize = MaxPageSize;
				else
					_pageSize = value;
			}
		}

	}
}
