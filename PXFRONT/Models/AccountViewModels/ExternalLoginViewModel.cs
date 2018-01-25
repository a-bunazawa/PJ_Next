#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace PXFRONT.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
