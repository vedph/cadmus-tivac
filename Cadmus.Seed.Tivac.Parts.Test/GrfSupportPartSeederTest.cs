using Cadmus.Core;
using Cadmus.Tivac.Parts;
using Fusi.Tools.Config;
using System;
using System.Reflection;
using Xunit;

namespace Cadmus.Seed.Tivac.Parts.Test
{
    public sealed class GrfSupportPartSeederTest
    {
        private static readonly PartSeederFactory _factory =
            TestHelper.GetFactory();
        private static readonly SeedOptions _seedOptions =
            _factory.GetSeedOptions();
        private static readonly IItem _item =
            _factory.GetItemSeeder().GetItem(1, "facet");

        [Fact]
        public void TypeHasTagAttribute()
        {
            Type t = typeof(GrfSupportPartSeeder);
            TagAttribute? attr = t.GetTypeInfo().GetCustomAttribute<TagAttribute>();
            Assert.NotNull(attr);
            Assert.Equal("seed.it.vedph.tivac.grf-support", attr!.Tag);
        }

        [Fact]
        public void Seed_Ok()
        {
            GrfSupportPartSeeder seeder = new();
            seeder.SetSeedOptions(_seedOptions);

            IPart part = seeder.GetPart(_item, null, _factory);

            Assert.NotNull(part);

            GrfSupportPart? p = part as GrfSupportPart;
            Assert.NotNull(p);

            TestHelper.AssertPartMetadata(p!);
        }
    }
}
