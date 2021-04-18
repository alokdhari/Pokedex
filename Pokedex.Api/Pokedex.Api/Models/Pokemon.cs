namespace Pokedex.Api.Controllers
{
    /// <summary>
    /// Defines the <see cref="Pokemon" />.
    /// </summary>
    public class Pokemon
    {
        /// <summary>
        /// Gets or sets the Habitat.
        /// </summary>
        public string Habitat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Is_Legendary.
        /// </summary>
        public bool IsLegendary { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the StandardDescription.
        /// </summary>
        public string StandardDescription { get; set; }
    }
}
