using System;

namespace CAArtStudio.Model.Abstract
{
	public abstract class Auditable : IAuditable
    {
        public int ID { set; get; }
        public DateTime Created { get; set; }
		public int CreatedBy { get; set; }
		public DateTime Modified { get; set; }
		public int ModifiedBy { get; set; }
		public bool IsDeleted { get; set; }
	}
}