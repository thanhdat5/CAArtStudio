using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class SliderResponseModel : Auditable
	{
		public string Image { get; set; }
		public int? DisplayOrder { get; set; }
		public string Link { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Target { get; set; }
		public UserResponseModel CreatedByUser { get; set; }
		public UserResponseModel ModifiedByUser { get; set; }
	}
}
