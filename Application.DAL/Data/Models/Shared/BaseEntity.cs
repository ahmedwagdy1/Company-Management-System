namespace Application.DAL.Data.Models.Shared
{
    public class BaseEntity // include the commen properties [Parent]
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } // User Id
        public DateTime? CreatedOn { get; set; } // The date time of creating record
        public int ModifiedBy { get; set; } // User Id
        public DateTime? ModifiedOn { get; set; } // The date time of modified record
        public bool IsDeleted { get; set; } // Soft Delete
    }
}
