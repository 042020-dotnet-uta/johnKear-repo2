using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharSheet
{
    public class FormLabel
    {
        [Key]
        public Guid FormLabelId { get; set; }

        [ForeignKey("FormTemplate")]
        public Guid FormTemplateId { get; set; }

        public int Index { get; set; }
        
        public string Value { get; set; }
    }
}