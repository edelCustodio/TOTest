using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOTest.Api.Models;
using TOTest.Api.Services;

namespace TOTest.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		private readonly ILogger<ImageController> _logger;
		private readonly IImageService _imageService;
		private readonly IMemoryCache _memoryCache;
		private readonly string imagesOnCacheKey = "imagesOnCacheKey";

		public ImageController(ILogger<ImageController> logger, IImageService imageService, IMemoryCache memoryCache)
		{
			_logger = logger;
			_imageService = imageService;
			_memoryCache = memoryCache;
		}

		public async Task<IActionResult> Get()
		{
			IEnumerable<ImageViewModel> imageCollection = null;

			// If found in cache, return cached data
			if (_memoryCache.TryGetValue(imagesOnCacheKey, out imageCollection))
			{
				return Ok(imageCollection);
			}

			imageCollection = await _imageService.GetImagesAsync();

			imageCollection = DuplicateData(imageCollection);

			// Set cache options
			var cacheOptions = new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromSeconds(30));

			_memoryCache.Set(imagesOnCacheKey, imageCollection, cacheOptions);

			return Ok(imageCollection);
		}

		private IEnumerable<ImageViewModel> DuplicateData(IEnumerable<ImageViewModel> imageViewModels)
		{
			var duplicatedList1 = imageViewModels.Select(s => new ImageViewModel { Company = $"{s.Company} 1", Image_List_Array = s.Image_List != null ? s.Image_List.Split('|') : new string[] { }, ListingID = s.ListingID }).ToList();
			var duplicatedList2 = imageViewModels.Select(s => new ImageViewModel { Company = $"{s.Company} 2", Image_List_Array = s.Image_List != null ? s.Image_List.Split('|') : new string[] { }, ListingID = s.ListingID }).ToList();

			duplicatedList1.Concat(duplicatedList2);

			return duplicatedList1;
		}
	}
}
