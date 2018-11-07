using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class CourseFeedbackRequestModel : Auditable
	{
		public int CourseID { get; set; }
		public int? StudentID { get; set; }
		public string Comment { get; set; }
	}
}
