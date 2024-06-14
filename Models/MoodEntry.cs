using System.ComponentModel.DataAnnotations;

namespace MoodTracker.Models
{
    public class MoodEntry
    {
        [Key]
        public int MoodEntryId { get; set; }
        public string UserId { get; set; }
        public int MoodId { get; set; }

        public DateTime EntryDate { get; set; }

        public string Notes { get; set; }


        public AppUser User { get; set; }
        public Mood Mood { get; set; }


    }
}
