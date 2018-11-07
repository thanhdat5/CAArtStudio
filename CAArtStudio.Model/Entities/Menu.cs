namespace CAArtStudio.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        [Required]
        [StringLength(250)]
        public string Text { get; set; }

        public int? ParentID { get; set; }

        public string Url { get; set; }

        [StringLength(20)]
        public string Target { get; set; }

        public int? DisplayOrder { get; set; }

        public bool ShowOnHome { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; }

        public int CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public int ModifiedBy { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
