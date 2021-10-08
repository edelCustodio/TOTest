using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TOTest.Api.Models;

namespace TOTest.Api.Services
{
	public interface IImageService
	{
		Task<IEnumerable<ImageViewModel>> GetImages();
		Task<IEnumerable<ImageViewModel>> GetImagesAsync();
	}
}
