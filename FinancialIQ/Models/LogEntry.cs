using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FinancialIQ.Models {
    public class LogEntry {
        [Required(ErrorMessage = "A category is required")]
        public string Category { get; set; }
        
        [Required(ErrorMessage = "A Description is required")]
        public string Description { get; set; }
        
        [Required(ErrorMessage = "A value is required")]
        public decimal Value { get; set; }

        public string Subcategory { get; set; }

        public string Direction { get; set; }

        public IEnumerable<string> DistinctCategories { get; set; }

        public IEnumerable<string> DistinctSubCategories { get; set; }
    }
}