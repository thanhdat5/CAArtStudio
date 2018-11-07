using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class ProductRequestModel : Auditable
	{
		public int? CourseID { get; set; }
		public int? UserID { get; set; }
		public string Image { get; set; }
	}
}
