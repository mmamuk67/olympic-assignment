using System.ComponentModel.DataAnnotations;

namespace olympic_assignment.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a ticket name.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a sprint number.")]
        [Range(1, 10, ErrorMessage = "Sprint number must be between 1 and 10.")]
        public int? SprintNumber { get; set; }

        [Required(ErrorMessage = "Please enter a point value.")]
        [Range(1, 100, ErrorMessage = "Point value must be between 1 and 100.")]
        public int? PointValue { get; set; }

        [Required(ErrorMessage = "Please enter a status.")]
        public string Status { get; set; } = string.Empty;
    }
}