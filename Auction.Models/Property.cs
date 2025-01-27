using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Auction.Models
{
    // First, update the Property model to include SellerId
    public class Property
    {
        [Key]
        public int PropertyId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Area in sq foot")]
        public double Size { get; set; }
        [Required]
        public int NumberOfRooms { get; set; }
        [Required]
        public int NumberOfBathrooms { get; set; }
        [Required]
        public int YearBuilt { get; set; }
        public int PropertyCategoryId { get; set; }
        [ForeignKey("PropertyCategoryId")]
        [ValidateNever]
        public virtual PropertyCategory? PropertyCategory { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        [ValidateNever]
        public string VideoUrl { get; set; }

        // Add SellerId
        [Required]
        public string SellerId { get; set; }
        [ForeignKey("SellerId")]
        [ValidateNever]
        public virtual ApplicationUser? Seller { get; set; }
    }

}
