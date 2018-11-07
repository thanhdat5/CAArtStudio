using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class MenuRequestViewModel: Auditable
	{
		public string Text { get; set; }

		public int? ParentID { get; set; }

		public string Url { get; set; }

		public string Target { get; set; }

		public int? DisplayOrder { get; set; }

		public bool ShowOnHome { get; set; }
	}
}
