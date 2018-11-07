using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class GroupRequestModel : Auditable
	{
		public string Name { get; set; }
		public bool IsActive { get; set; }
	}
}
