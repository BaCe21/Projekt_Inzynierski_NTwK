namespace TrikProjekt56.Models
{
    public class Religion
    {
        [Key]
        public int Id { get; set; }

        [Remote("IsNameExisting", "Religion", AdditionalFields = "Id", ErrorMessage = "Name already exists in database.")]
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
