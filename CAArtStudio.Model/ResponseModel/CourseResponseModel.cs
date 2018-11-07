using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class CourseResponseModel : Auditable
	{
		public string Name { get; set; }
		public string PhotoImage { get; set; }
		public string ShortDescription { get; set; }
		public string Intro { get; set; }
		public string Details { get; set; }
		public string Promotion { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescriptions { get; set; }
		public string MetaKeywords { get; set; }
		public bool? IsPopular { get; set; }
		public int ViewCount { get; set; }
		public bool IsActive { get; set; }
		public int? CategoryID { get; set; }
		public UserResponseModel CreatedByUser { get; set; }
		public UserResponseModel ModifiedByUser { get; set; }
	}
}
