using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Tivac.Parts;

namespace Cadmus.Tivac.Parts.Test
{
    public sealed class GrfLocationPartTest
    {
        private static GrfLocationPart GetPart()
        {
            GrfLocationPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (GrfLocationPart)seeder.GetPart(item, null, null);
        }

        private static GrfLocationPart GetEmptyPart()
        {
            return new GrfLocationPart
            {
                ItemId = Guid.NewGuid().ToString(),
                RoleId = "some-role",
                CreatorId = "zeus",
                UserId = "another",
            };
        }

        [Fact]
        public void Part_Is_Serializable()
        {
            GrfLocationPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            GrfLocationPart part2 = TestHelper.DeserializePart<GrfLocationPart>(json)!;

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);
        }

        [Fact]
        public void GetDataPins_Tag_1()
        {
            GrfLocationPart part = GetEmptyPart();
            part.Place = "place1";
            part.Area = "area1";
            part.District = "district1";

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(3, pins.Count);

            DataPin? pin = pins.Find(p => p.Name == "place" && p.Value == "place1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "area" && p.Value == "area1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "district" && p.Value == "district1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
