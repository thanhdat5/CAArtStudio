namespace CAArtStudio.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseSlider")]
    public partial class CourseSlider
    {
        public int ID { get; set; }

        public int CourseID { get; set; }

        [Required]
        public string Image { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; }

        public int CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public int ModifiedBy { get; set; }

        public virtual Course Course { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
