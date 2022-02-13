using Cadmus.Core;
using Cadmus.Seed.Tivac.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cadmus.Tivac.Parts.Test
{
    public sealed class GrfWritingPartTest
    {
        private static GrfWritingPart GetPart()
        {
            GrfWritingPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (GrfWritingPart)seeder.GetPart(item, null, null);
        }

        private static GrfWritingPart GetEmptyPart()
        {
            return new GrfWritingPart
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
            GrfWritingPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            GrfWritingPart part2 = TestHelper.DeserializePart<GrfWritingPart>(json)!;

            Assert.Equal(part.Id, part2.Id);
            Assert.Equal(part.TypeId, part2.TypeId);
            Assert.Equal(part.ItemId, part2.ItemId);
            Assert.Equal(part.RoleId, part2.RoleId);
            Assert.Equal(part.CreatorId, part2.CreatorId);
            Assert.Equal(part.UserId, part2.UserId);
        }

        [Fact]
        public void GetDataPins_Ok()
        {
            GrfWritingPart part = GetEmptyPart();
            part.System = "latn";
            part.Type = "capital";
            part.Language = "lat";
            part.IsPoetic = true;
            part.Technique = "ink";
            part.Tool = "pen";
            part.FigType = "cross";
            part.ContentFeatures.Add("cross");
            part.ContentFeatures.Add("heart");
            part.FrameType = "rectangle";
            part.Casing = "lower";
            part.RowCount = 3;

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(10, pins.Count);

            DataPin? pin = pins.Find(p => p.Name == "system" && p.Value == "latn");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "type" && p.Value == "capital");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "language" && p.Value == "lat");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "poetic" && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "technique" && p.Value == "ink");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "tool" && p.Value == "pen");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "fig-type" && p.Value == "cross");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "feature" && p.Value == "cross");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "feature" && p.Value == "heart");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "row-count" && p.Value == "3");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
