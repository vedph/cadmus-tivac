using Bogus;
using Cadmus.Core;
using Cadmus.Mat.Bricks;
using Cadmus.Tivac.Parts;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Tivac.Parts
{
    /// <summary>
    /// Seeder for <see cref="GrfSupportPart"/>.
    /// Tag: <c>seed.it.vedph.tivac.grf-support</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.tivac.grf-support")]
    public sealed class GrfSupportPartSeeder : PartSeederBase
    {
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

            string[] fnn = new[] { "street", "house" };

            GrfSupportPart part = new Faker<GrfSupportPart>()
               .RuleFor(p => p.OriginalFn, f => f.PickRandom(fnn))
               .RuleFor(p => p.CurrentFn, f => f.PickRandom(fnn))
               .RuleFor(p => p.ObjectType,
                    f => f.PickRandom("street", "bridge", "well"))
               .RuleFor(p => p.SupportType,
                    f => f.PickRandom("street", "door"))
               .RuleFor(p => p.IsIndoor, f => f.Random.Bool())
               .RuleFor(p => p.Material,
                    f => f.PickRandom("concrete", "wood", "stone"))
               .RuleFor(p => p.Size,
                    f => new PhysicalSize
                    {
                        W = new PhysicalDimension
                        {
                            Value = f.Random.Number(5, 30),
                            Unit = "cm"
                        },
                        H = new PhysicalDimension
                        {
                            Value = f.Random.Number(5, 30),
                            Unit = "cm"
                        }
                    })
               .RuleFor(p => p.State, f => f.Lorem.Sentence())
               .RuleFor(p => p.LastViewed, f => f.Date.Past())
               .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
