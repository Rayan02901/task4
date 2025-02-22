﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Auction.Models
{
    public class PropertyCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Property Category Name")]
        [MaxLength(50)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "It must be between 1 to 100")]
        public int DisplayOrder { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
