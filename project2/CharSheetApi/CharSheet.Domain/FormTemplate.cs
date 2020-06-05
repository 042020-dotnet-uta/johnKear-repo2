using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CharSheet.Domain
{
    public class FormTemplate
    {
        [Key]
        public Guid FormTemplateId { get; set; }

        [ForeignKey("Template")]
        public Guid TemplateId { get; set; }

        [ForeignKey("FormPosition")]
        public Guid FormPositionId { get; set; }
        public FormPosition FormPosition { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public ICollection<FormLabel> FormLabels { get ; set; }
    }
}