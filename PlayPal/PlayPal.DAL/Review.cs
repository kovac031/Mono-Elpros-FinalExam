namespace PlayPal.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        public Guid Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Comment { get; set; }

        public bool IsApproved { get; set; }

        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }

        public bool IsActive { get; set; }

        public Guid CoreUserId { get; set; }

        public Guid BoardGameId { get; set; }

        public virtual BoardGame BoardGame { get; set; }

        public virtual CoreUser CoreUser { get; set; }

        public virtual CoreUser CoreUser1 { get; set; }

        public virtual CoreUser CoreUser2 { get; set; }
    }
}
