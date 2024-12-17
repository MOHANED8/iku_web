using System.ComponentModel.DataAnnotations;

namespace E_Commmerce.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Role name is required.")]
        [StringLength(100, ErrorMessage = "Role name cannot exceed 100 characters.")]
        public string Name { get; set; }
    }

}
