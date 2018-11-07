using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class CourseFeedbackResponseModel : Auditable
	{
		public int CourseID { get; set; }
		public string CourseName { get; set; }
		public int? StudentID { get; set; }
		public string StudentName { get; set; }
		public string StudentImage { get; set; }
		public string Comment { get; set; }
		public UserResponseModel CreatedByUser { get; set; }
		public UserResponseModel ModifiedByUser { get; set; }
	}
}
