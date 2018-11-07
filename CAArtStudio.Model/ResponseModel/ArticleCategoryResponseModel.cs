using CAArtStudio.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAArtStudio.Model.ResponseModel
{
	public class ArticleCategoryResponseModel : Auditable
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string PhotoImage { get; set; }
		public int? DisplayOrder { get; set; }
		public int? ParentID { get; set; }
		public ArticleCategoryResponseModel ParentCategory { get; set; }
		public string MetaTitle { get; set; }
		public string MetaDescriptions { get; set; }
		public string MetaKeywords { get; set; }
		public UserResponseModel CreatedByUser { get; set; }
		public UserResponseModel ModifiedByUser { get; set; }
	}
}
