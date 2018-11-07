using System;

namespace CAArtStudio.Model.Abstract
{
    public interface IAuditable
    {
        int ID { set; get; }  
		DateTime Created { set; get; }
		int CreatedBy { set; get; }
		DateTime Modified { set; get; }
		int ModifiedBy { set; get; }
		bool IsDeleted { set; get; }
	}
}