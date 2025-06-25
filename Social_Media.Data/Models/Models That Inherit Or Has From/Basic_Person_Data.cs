using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Social_Media.Data.Models.Models_That_Inherit_From
{
    [Owned]
    public class Basic_Person_Data
    {
        [MaxLength(100)]
        public string FirstNameInEnglish { get; set; }
        [MaxLength(100)]
        public string? LastNameInEnglish { get; set; }
        [MaxLength(100)]
        public string FirstNameInAabic { get; set; }
        [MaxLength(100)]
        public string? LastNameInArabic { get; set; }
    }
}
