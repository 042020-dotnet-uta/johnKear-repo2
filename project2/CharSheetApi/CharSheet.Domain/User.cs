using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharSheet.Domain
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [ForeignKey("Login")]
        public Guid LoginId { get; set; }
        public Login Login { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
