using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharSheet.Domain
{
    public class Login
    {
        [Key]
        public Guid LoginId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public byte[] Salt { get; set; }

        public int IterationCount { get; set; }
        
        public string Hashed { get; set; }
    }
}