using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Config;

namespace Cadmus.Tivac.Parts
{
    /// <summary>
    /// Graffiti writing data part.
    /// <para>Tag: <c>it.vedph.tivac.grf-writing</c>.</para>
    /// </summary>
    [Tag("it.vedph.tivac.grf-writing")]
    public sealed class GrfWritingPart : PartBase
    {
        /// <summary>
        /// Gets or sets the writing system (usually ISO 15924, lowercased).
        /// </summary>
        public string System { get; set; }

        /// <summary>
        /// Gets or sets the writing type.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the text language (usually ISO 639-3).
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is a poetic text.
        /// </summary>
        public bool IsPoetic { get; set; }

        /// <summary>
        /// Gets or sets the writing technique.
        /// </summary>
        public string Technique { get; set; }

        /// <summary>
        /// Gets or sets the writing tool.
        /// </summary>
        public string Tool { get; set; }

        /// <summary>
        /// Gets or sets the type of the figurative element if any.
        /// </summary>
        public string FigType { get; set; }

        /// <summary>
        /// Gets or sets the content features: these are usually drawn from a
        /// closed set, e.g. has-text, has-digits, has-punctuation, has-ligature,
        /// has-abbreviation, has-monogram, has-single-letter, has-undefined-letter,
        /// has-undefined-drawing.
        /// </summary>
        public List<string> ContentFeatures { get; set; }

        /// <summary>
        /// Gets or sets the type of the frame.
        /// </summary>
        public string FrameType { get; set; }

        /// <summary>
        /// Gets or sets the casing type (e.g. lowercase, uppercase, mix).
        /// </summary>
        public string Casing { get; set; }

        /// <summary>
        /// Gets or sets the count of rows.
        /// </summary>
        public short RowCount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GrfWritingPart"/> class.
        /// </summary>
        public GrfWritingPart()
        {
            ContentFeatures = new List<string>();
        }

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

            builder.AddValue("system", System);
            builder.AddValue("type", Type);
            builder.AddValue("language", Language);
            builder.AddValue("poetic", IsPoetic);
            builder.AddValue("technique", Technique);
            builder.AddValue("tool", Tool);
            builder.AddValue("fig-type", FigType);
            if (ContentFeatures?.Count > 0)
                builder.AddValues("feature", ContentFeatures);
            if (RowCount > 0)
                builder.AddValue("row-count", RowCount);

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
                    "system",
                    "The writing system."),
                 new DataPinDefinition(DataPinValueType.String,
                    "type",
                    "The writing type."),
                 new DataPinDefinition(DataPinValueType.String,
                    "language",
                    "The language."),
                 new DataPinDefinition(DataPinValueType.String,
                    "poetic",
                    "True if poetic, false if prose."),
                 new DataPinDefinition(DataPinValueType.String,
                    "technique",
                    "The writing technique."),
                 new DataPinDefinition(DataPinValueType.String,
                    "tool",
                    "The writing tool."),
                 new DataPinDefinition(DataPinValueType.String,
                    "fig-type",
                    "The figurative type."),
                 new DataPinDefinition(DataPinValueType.String,
                    "feature",
                    "The feature(s) present."),
                 new DataPinDefinition(DataPinValueType.Integer,
                    "row-count",
                    "The rows count."),
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

            sb.Append("[GrfWriting] ").Append(System).Append(' ').Append(Type)
                .Append(": ").Append(RowCount);

            return sb.ToString();
        }
    }
}
