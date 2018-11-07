namespace CAArtStudio.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        public int ID { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        public string PhotoImage { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [Required]
        public string Details { get; set; }

        [Required]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string MetaDescriptions { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        public bool? IsPopular { get; set; }
		
        public int ViewCount { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime Created { get; set; }

        public int CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public int ModifiedBy { get; set; }

        public int? CategoryID { get; set; }

        public virtual ArticleCategory ArticleCategory { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
