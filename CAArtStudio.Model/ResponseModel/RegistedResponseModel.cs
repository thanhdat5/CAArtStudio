using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class RegistedResponseModel : Auditable
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public int CourseID { get; set; }
		public string Note { get; set; }
		public UserResponseModel CreatedByUser { get; set; }
		public UserResponseModel ModifiedByUser { get; set; }
	}
}
