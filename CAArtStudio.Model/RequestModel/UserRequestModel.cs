using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class UserRequestModel : Auditable
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string FullName { get; set; }
		public string PhotoImage { get; set; }
		public string JobTitle { get; set; }
		public string AboutMe { get; set; }
		public string Skills { get; set; }
		public string Email { get; set; }
		public string Facebook { get; set; }
		public string Twitter { get; set; }
		public string GooglePlus { get; set; }
		public string LinkedIn { get; set; }
		public bool IsActive { get; set; }
		public int? GroupID { get; set; }
	}
}
