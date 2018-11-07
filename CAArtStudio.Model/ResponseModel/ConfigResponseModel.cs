using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class ConfigResponseModel : Auditable
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public UserResponseModel CreatedByUser { get; set; }
		public UserResponseModel ModifiedByUser { get; set; }
	}
}
