namespace PlayPal.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BoardGame")]
    public partial class BoardGame
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardGame()
        {
            RentedBoardGame = new HashSet<RentedBoardGame>();
            Review = new HashSet<Review>();
        }

        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public decimal Price { get; set; }

        [Column(TypeName = "text")]
        public string Description { get; set; }

        public int NumberOfCopies { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public Guid CreatedByUserId { get; set; }

        public Guid UpdatedByUserId { get; set; }

        public bool IsActive { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual CoreUser CoreUser { get; set; }

        public virtual CoreUser CoreUser1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RentedBoardGame> RentedBoardGame { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Review { get; set; }
    }
}
