using Bogus;
using Cadmus.Core;
using Cadmus.Tivac.Parts;
using Fusi.Tools.Config;
using System;

namespace Cadmus.Seed.Tivac.Parts
{
    /// <summary>
    /// Seeder for <see cref="GrfLocationPart"/>.
    /// Tag: <c>seed.it.vedph.tivac.grf-location</c>.
    /// </summary>
    /// <seealso cref="PartSeederBase" />
    [Tag("seed.it.vedph.tivac.grf-location")]
    public sealed class GrfLocationPartSeeder : PartSeederBase
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

            // TODO: should we use thesauri?
            GrfLocationPart part = new Faker<GrfLocationPart>()
               .RuleFor(p => p.Place, f => f.Address.City())
               .RuleFor(p => p.Area, f => "A" + f.Random.Number(1, 10))
               .RuleFor(p => p.District, f => "D" + f.Random.Number(1, 10))
               .RuleFor(p => p.Location, f => f.Address.SecondaryAddress())
               .Generate();
            SetPartMetadata(part, roleId, item);

            return part;
        }
    }
}
