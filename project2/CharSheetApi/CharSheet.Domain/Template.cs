using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CharSheet.Domain
{
    public class Template
    {
        [Key]
        public Guid TemplateId { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public ICollection<FormTemplate> FormTemplates { get; set; }
    }
}