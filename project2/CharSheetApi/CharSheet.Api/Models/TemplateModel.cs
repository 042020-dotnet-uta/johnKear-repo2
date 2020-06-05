using System;
using System.Collections.Generic;

namespace CharSheet.Api.Models
{
    public class TemplateModel
    {
        public Guid TemplateId { get; set; }
        public string Name { get; set; }
        public IEnumerable<FormTemplateModel> FormTemplates { get; set; }
    }
}