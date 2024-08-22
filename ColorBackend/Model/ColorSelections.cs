namespace ColorBackend.Model
{
    /// <summary>
    /// Represents a record of a color selection made by a user.
    /// </summary>
    public class ColorSelections
    {
        /// <summary>
        /// Gets or sets the unique identifier for the color selection record.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the selected color.
        /// </summary>
        public int ColorID { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the user who made the selection.
        /// </summary>
        public string? UserIP { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the color was selected.
        /// </summary>
        public DateTime SelectionTime { get; set; }
    }
}
