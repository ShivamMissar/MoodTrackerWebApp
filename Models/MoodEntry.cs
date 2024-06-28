using System.ComponentModel.DataAnnotations;

namespace MoodTracker.Models
{
    public class MoodEntry
    {
        [Key]
        public int MoodEntryId { get; set; }
        public string UserId { get; set; }

        public string MoodName { get; set; }

        public string MoodDescription { get; set; }

        public string MoodColour { get; set; }

        public DateTime EntryDate { get; set; }

        public string Notes{ get; set; }

        public string? mood_insight { get; set; }

        public string? recommendation { get; set; }


        public AppUser User { get; set; }
    


    }
}
