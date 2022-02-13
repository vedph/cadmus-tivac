using Bogus;
using Cadmus.Core;
using Cadmus.Tivac.Parts;
using Fusi.Tools.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadmus.Seed.Tivac.Parts
{
    /// <summary>
    /// Seeder for <see cref="GrfWritingPart"/>.
    /// Tag: <c>seed.it.vedph.tivac.grf-writing</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.tivac.grf-writing")]
    public sealed class GrfWritingPartSeeder : PartSeederBase
    {
        private static IList<string> GetFeatures(int count, Faker f)
        {
            string[] features = new string[]
            {
                "text", "digit", "punctuation", "ligature",
                "abbreviation", "monogram", "single-letter", "undef-letter",
                "undef-drawing"
            };
            if (count >= features.Length) return features;

            HashSet<string> picked = new HashSet<string>();
            while (picked.Count < count)
            {
                picked.Add(f.PickRandom(features));
            }
            return picked.ToList();
        }

        /// <summary>
        /// Creates and seeds a new part.
        /// </summary>
        /// <param name="item">The item this part should belong to.</param>
        /// <param name="roleId">The optional part role ID.</param>
        /// <param name="factory">The part seeder factory. This is used
        /// for layer parts, which need to seed a set of fragments.</param>
        /// <returns>A new part.</returns>
        /// <exception cref="ArgumentNullException">item or factory</exception>
        public override IPart GetPart(IItem item, string roleId,
            PartSeederFactory factory)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            GrfWritingPart part = new Faker<GrfWritingPart>()
               .RuleFor(p => p.System, f => f.PickRandom("latn", "grek"))
               .RuleFor(p => p.Type, f => f.PickRandom("-", "capital"))
               .RuleFor(p => p.Language, f => f.PickRandom("lat", "grc"))
               .RuleFor(p => p.IsPoetic, f => f.Random.Bool())
               .RuleFor(p => p.Technique,
                    f => f.PickRandom("graffiti", "ink", "paint"))
               .RuleFor(p => p.Tool, f => f.PickRandom("-", "blade", "pen"))
               .RuleFor(p => p.FigType,
                    f => f.PickRandom("-", "cross", "heart", "geometric"))
               .RuleFor(p => p.ContentFeatures,
                    f => GetFeatures(f.Random.Number(1, 3), f))
               // TODO replace these fake values
               .RuleFor(p => p.FrameType,
                    f => f.PickRandom("-", "rectangle", "ellipse"))
               .RuleFor(p => p.Casing,
                    f => f.PickRandom("-", "lower", "upper", "mixed"))
               .RuleFor(p => p.RowCount, f => f.Random.Short(1, 10))
               .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
