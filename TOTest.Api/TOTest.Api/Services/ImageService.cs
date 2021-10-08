using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOTest.Api.Infraestructure;
using TOTest.Api.Models;

namespace TOTest.Api.Services
{
	public class ImageService : IImageService
	{
		private readonly IHttpClient _httpClient;

		public ImageService(IHttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public Task<IEnumerable<ImageViewModel>> GetImages()
		{
			return _httpClient.Get();
		}

		public Task<IEnumerable<ImageViewModel>> GetImagesAsync()
		{
			return _httpClient.Get();
		}
	}
}
