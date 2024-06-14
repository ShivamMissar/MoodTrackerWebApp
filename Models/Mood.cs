using System.ComponentModel.DataAnnotations;

namespace MoodTracker.Models
{
    public class Mood
    {
        [Key]
        public int MoodId { get; set; }

        public string MoodName { get; set; }

        public string MoodDescription { get; set; }

        public string MoodColour { get; set; }


    }
}
