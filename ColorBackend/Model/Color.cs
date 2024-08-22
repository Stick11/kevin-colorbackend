namespace ColorBackend.Model
{
    /// <summary>
    /// Represents a color entity in the system.
    /// </summary>
    public class Color
    {
        /// <summary>
        /// Gets or sets the unique identifier for the color.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the color.
        /// </summary>
        public string? Name { get; set; }
    }
}
