using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CharSheet.Domain
{
    public class FormInput
    {
        [Key]
        public Guid FormInputId { get; set; }

        [ForeignKey("FormInputGroup")]
        public Guid FormInputGroupId { get; set; }

        public int Index { get; set; }

        public string Value { get; set; }
    }
}