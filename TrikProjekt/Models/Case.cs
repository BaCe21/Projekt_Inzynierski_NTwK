using System.ComponentModel.DataAnnotations.Schema;
namespace TrikProjekt56.Models

{
    public class Case
    {
        [Key]
        [StringLength(6)]
        public string Code { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "StartDate")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Case closed?")]
        public bool isClosed { get; set; }

        [Required]
        [ForeignKey("Categories")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public virtual Category Categories { get; set; }

        [Required]
        [ForeignKey("Locations")]
        [Display(Name = "Location")]
        public int? LocationId { get; set; }
        public virtual Location Locations { get; set; }

        [Required]
        [ForeignKey("Ages")]
        [Display(Name = "Age")]
        public int? AgeId { get; set; }
        public virtual Age Ages { get; set; }

        [Required]
        [ForeignKey("Hairs")]
        [Display(Name = "Hair")]
        public int? HairId { get; set; }
        public virtual Hair Hairs { get; set; }

        [Required]
        [ForeignKey("Genders")]
        [Display(Name = "Gender")]
        public int? GenderId { get; set; }
        public virtual Gender Genders { get; set; }

        [Required]
        [ForeignKey("Religions")]
        [Display(Name = "Religion")]
        public int? ReligionId { get; set; }
        public virtual Religion Religions { get; set; }

        [Required]
        [ForeignKey("Educations")]
        [Display(Name = "Education")]
        public int? EducationId { get; set; }
        public virtual Education Educations { get; set; }

        [Required]
        [ForeignKey("Heights")]
        [Display(Name = "Height")]
        public int? HeightId { get; set; }
        public virtual Height Heights { get; set; }

        [Required]
        [ForeignKey("Weights")]
        [Display(Name = "Weight")]
        public int? WeightId { get; set; }
        public virtual Weight Weights { get; set; }
    }
}
