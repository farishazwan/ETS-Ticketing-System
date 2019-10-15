using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class UserResponses
    {
    }

    public class Ticket
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please Enter Your Name")]
        public string Name { get; set; }

        [Display(Name = "IC Number")]
        [Required(ErrorMessage = "Please Enter IC/Passport Number")]
        public string ICNum { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Origin")]
        [Required(ErrorMessage = "Please Enter Current Location")]
        public string FromDestination { get; set; }

        [Display(Name = "Destination")]
        [Required(ErrorMessage = "Please Enter Destination")]
        public string ToDestination { get; set; }

        [Display(Name = "Type of User")]
        [Required(ErrorMessage = "Please Enter Type of User")]
        public string TypeOfUser { get; set; }

        [Display(Name = "Number of Ticket")]
        [Required(ErrorMessage = "Please Enter Number of Ticket")]
        public int NumOfTicket { get; set; }

        [Display(Name = "Type of Trip")]
        [Required(ErrorMessage = "Please Enter Type of Trip")]
        public string NumOfWay { get; set; }

        [Display(Name = "Total Price")]
        public double TotalPrice { get; set; }

        [Display(Name = "Date Purchased")]
        public DateTime DatePurchased { get; set; }

        public int UserId { get; set; }
    }

    public class UserAccount
    {

        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Enter Preferred Password")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }

        public Boolean Enabled { get; set; }

    

    }

    public class UserLogin
    {
        [Required(ErrorMessage = "Please Enter Email Address")]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }


    }

    public class User
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ProfileId { get; set; }

        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please Enter Your First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please Enter Your Last Name")]
        public string LastName { get; set; }

        [Display(Name = "IC Number")]
        [Required(ErrorMessage = "Please Enter Your IC/Passport Number")]
        public string ICNum { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please Enter Your Phone Number")]
        public string PhoneNum { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please Enter Your Address")]
        public string Address { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "Please Enter Your Image")]
        public string profileImg { get; set; }
        
    }

}