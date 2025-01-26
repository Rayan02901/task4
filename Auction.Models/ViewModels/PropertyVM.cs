using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Auction.Models.ViewModels
{
    public class PropertyVM
    {
        public Property Property { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> PropertyCategories { get; set; }
        
    }
}