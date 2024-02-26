using System.ComponentModel.DataAnnotations;

namespace MyProWeb.Models.Domain
{
    public class Order
    {
        [Required(ErrorMessage ="Can't blank this field!")]
        public string Name {  get; set; }

        [Required(ErrorMessage = "Can't blank this field!")]

        public string Address { get; set; }
        [Required(ErrorMessage = "Can't blank this field!")]
        public string PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
       
        public string TypePayment { get; set; }

    }
}
