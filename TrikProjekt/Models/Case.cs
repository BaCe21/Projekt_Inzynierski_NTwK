﻿using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("Ages")]
        [Display(Name = "Age")]
        public int? AgeId { get; set; }
        public virtual Age Ages { get; set; }

        [ForeignKey("Hairs")]
        [Display(Name = "Hair")]
        public int? HairId { get; set; }
        public virtual Hair Hairs { get; set; }

        [ForeignKey("DistFeatures")]
        [Display(Name = "DistFeature")]
        public int? DistFeatureId { get; set; }
        public virtual DistFeature DistFeatures { get; set; }

        [ForeignKey("Corpses")]
        [Display(Name = "Other")]
        public int? CorpseId { get; set; }
        public virtual Corpse Corpses { get; set; }

        [ForeignKey("Educations")]
        [Display(Name = "Education")]
        public int? EducationId { get; set; }
        public virtual Education Educations { get; set; }

        [ForeignKey("Heights")]
        [Display(Name = "Height")]
        public int? HeightId { get; set; }
        public virtual Height Heights { get; set; }

        [ForeignKey("Weights")]
        [Display(Name = "Weight")]
        public int? WeightId { get; set; }
        public virtual Weight Weights { get; set; }
    }
}
