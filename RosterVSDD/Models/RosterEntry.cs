using System;
using System.ComponentModel.DataAnnotations;

namespace RosterVSDD.Models
{
    public class RosterEntry
    {
        [Required]
        [StringLength(256)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(256)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(256)]
        public string Major { get; set; } = string.Empty;

        [Required]
        [StringLength(128)]
        public string FavoriteKeyboardShortcut { get; set; } = string.Empty;

        [Required]
        [StringLength(128)]
        public string ShortcutContext { get; set; } = string.Empty;

        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    }
}
