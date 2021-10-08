using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TOTest.Api.Models
{
	public class ImageViewModel
	{
		[Key]
		public int ListingID { get; set; }
		public string Company { get; set; }
		public string Image_List { get; set; }
	}
}
