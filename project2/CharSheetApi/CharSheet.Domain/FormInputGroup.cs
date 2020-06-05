using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CharSheet.Domain
{
    public class FormInputGroup
    {
        [Key]
        public Guid FormInputGroupId { get; set; }

        [ForeignKey("Sheet")]
        public Guid SheetId { get; set; }

        [ForeignKey("FormTemplate")]
        public Guid FormTemplateId { get; set; }
        public FormTemplate FormTemplate { get; set; }

        public ICollection<FormInput> FormInputs { get; set; }
    }
}