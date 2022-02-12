using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Tivac.Parts
{
    /// <summary>
    /// Graffiti location.
    /// <para>Tag: <c>it.vedph.tivac.grf-location</c>.</para>
    /// </summary>
    [Tag("it.vedph.tivac.grf-location")]
    public sealed class GrfLocationPart : PartBase
    {
        /// <summary>
        /// Gets or sets the place. Usually this is an ID.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Gets or sets the area. Usually this is an ID.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the district. Usually this is an ID.
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Gets or sets a human-friendly specific location indication.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Get all the key=value pairs (pins) exposed by the implementor.
        /// </summary>
        /// <param name="item">The optional item. The item with its parts
        /// can optionally be passed to this method for those parts requiring
        /// to access further data.</param>
        /// <returns>The pins.</returns>
        public override IEnumerable<DataPin> GetDataPins(IItem item = null)
        {
            DataPinBuilder builder = new DataPinBuilder();

            builder.AddValue("place", Place);
            builder.AddValue("area", Area);
            builder.AddValue("district", District);

            return builder.Build(this);
        }

        /// <summary>
        /// Gets the definitions of data pins used by the implementor.
        /// </summary>
        /// <returns>Data pins definitions.</returns>
        public override IList<DataPinDefinition> GetDataPinDefinitions()
        {
            return new List<DataPinDefinition>(new[]
            {
                new DataPinDefinition(DataPinValueType.String,
                    "place",
                    "The place."),
                new DataPinDefinition(DataPinValueType.String,
                    "area",
                    "The area."),
                new DataPinDefinition(DataPinValueType.String,
                    "district",
                    "The district."),
            });
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[GrfLocation]");

            if (!string.IsNullOrEmpty(Place)) sb.Append(' ').Append(Place);
            if (!string.IsNullOrEmpty(Area)) sb.Append(", ").Append(Area);
            if (!string.IsNullOrEmpty(District)) sb.Append(", ").Append(District);

            return sb.ToString();
        }
    }
}
