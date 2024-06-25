using System.ComponentModel.DataAnnotations;

namespace MoodTracker.Models
{
    public class MoodUnderstanding
    {
        [Key]
        public int Id { get; set; }

        public string? mood_insight { get; set; }

        public string? recommendation { get; set; }
    }
}
