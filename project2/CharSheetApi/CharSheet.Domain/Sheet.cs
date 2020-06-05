using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CharSheet.Domain
{
    public class Sheet
    {
        [Key]
        public Guid SheetId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public ICollection<FormInputGroup> FormInputGroups { get; set; }
    }
}