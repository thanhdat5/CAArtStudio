using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class MenuResponseModel : Auditable
	{
		public string Text { get; set; }
		public int? ParentID { get; set; }
		public string ParentName { get; set; }
		public string Url { get; set; }
		public string Target { get; set; }
		public int? DisplayOrder { get; set; }
		public bool ShowOnHome { get; set; }
		public UserResponseModel CreatedByUser { get; set; }
		public UserResponseModel ModifiedByUser { get; set; }
	}
}
