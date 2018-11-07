using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class MenuResponseViewModel
	{
		public int ID { get; set; }

		public string Text { get; set; }

		public int? ParentID { get; set; }

		public string ParentName { get; set; }

		public string Url { get; set; }

		public string Target { get; set; }

		public int? DisplayOrder { get; set; }

		public bool ShowOnHome { get; set; }

		public bool IsDeleted { get; set; }

		public DateTime Created { get; set; }

		public int CreatedBy { get; set; }

		public string CreatedByName { get; set; }

		public DateTime Modified { get; set; }

		public int ModifiedBy { get; set; }

		public string ModifiedByName { get; set; }
	}
}
