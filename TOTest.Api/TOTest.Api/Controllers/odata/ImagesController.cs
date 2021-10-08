using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOTest.Api.Models;
using TOTest.Api.Services;

namespace TOTest.Api.Controllers.odata
{
	[ODataRoutePrefix("Images")]
	public class ImagesController : ODataController
	{
		private readonly IImageService _imageService;
		private readonly IMemoryCache _memoryCache;
		private readonly string imagesOnCacheODataKey = "imagesOnCacheODataKey";

		public ImagesController(IImageService imageService, IMemoryCache memoryCache)
		{
			_imageService = imageService;
			_memoryCache = memoryCache;
		}

		[HttpGet]
		[EnableQuery]
		public async Task<IActionResult> Get()
		{
			IEnumerable<ImageViewModel> imageCollection = null;

			// If found in cache, return cached data
			if (_memoryCache.TryGetValue(imagesOnCacheODataKey, out imageCollection))
			{
				return Ok(imageCollection);
			}

			imageCollection = await _imageService.GetImagesAsync();

			imageCollection = DuplicateData(imageCollection);

			// Set cache options
			var cacheOptions = new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromSeconds(30));

			_memoryCache.Set(imagesOnCacheODataKey, imageCollection, cacheOptions);

			return Ok(imageCollection);
		}

		private IEnumerable<ImageViewModel> DuplicateData(IEnumerable<ImageViewModel> imageViewModels)
		{
			var duplicatedList1 = imageViewModels.Select(s => new ImageViewModel { Company = $"{s.Company} 1", Image_List = s.Image_List, ListingID = s.ListingID }).ToList();
			var duplicatedList2 = imageViewModels.Select(s => new ImageViewModel { Company = $"{s.Company} 2", Image_List = s.Image_List, ListingID = s.ListingID }).ToList();

			duplicatedList1.AddRange(duplicatedList2);

			return duplicatedList1;
		}
	}
}
