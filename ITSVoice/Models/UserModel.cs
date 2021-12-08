using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITSVoice.Models
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; } 
        public string Fullname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime ExpiryDateTime { get; set; }
        public string APIKey { get; set; }
        [Required]
        public string UserType { get; set; }
        public int? ParentAccountId { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public decimal BalanceAmount { get; set; }
        [Required]
        public decimal CallRateAmount { get; set; }
        public string AuthorizedIP { get; set; }
    }
}