using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITSVoice.Models
{
    public class UserBalanceRechargeModel
    {
        public int UserId { get; set; }
        [Required]
        public int Action { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public int Balance { get; set; } 
    }
}