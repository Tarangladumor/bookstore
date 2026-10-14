using Microsoft.AspNetCore.Mvc.Rendering;

namespace bookStore.Models.ViewModels
{
    public class RoleManagerVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}