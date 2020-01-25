using System.ComponentModel.DataAnnotations;

namespace ClassifiedAds.IdentityServer.Manage.Models
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
