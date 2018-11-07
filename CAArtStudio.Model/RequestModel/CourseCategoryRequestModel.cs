using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class CourseCategoryRequestModel : Auditable
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string PhotoImage { get; set; }
		public int? ParentID { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescriptions { get; set; }
		public string MetaKeywords { get; set; }
	}
}
