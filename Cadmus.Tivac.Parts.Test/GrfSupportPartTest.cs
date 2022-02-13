using System;
using Xunit;
using Cadmus.Core;
using System.Collections.Generic;
using System.Linq;
using Cadmus.Seed.Tivac.Parts;
using Cadmus.Mat.Bricks;

namespace Cadmus.Tivac.Parts.Test
{
    public sealed class GrfSupportPartTest
    {
        private static GrfSupportPart GetPart()
        {
            GrfSupportPartSeeder seeder = new();
            IItem item = new Item
            {
                FacetId = "default",
                CreatorId = "zeus",
                UserId = "zeus",
                Description = "Test item",
                Title = "Test Item",
                SortKey = ""
            };
            return (GrfSupportPart)seeder.GetPart(item, null, null);
        }

        private static GrfSupportPart GetEmptyPart()
        {
            return new GrfSupportPart
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
            GrfSupportPart part = GetPart();

            string json = TestHelper.SerializePart(part);
            GrfSupportPart part2 = TestHelper.DeserializePart<GrfSupportPart>(json)!;

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
            GrfSupportPart part = GetEmptyPart();
            part.OriginalFn = "house";
            part.CurrentFn = "street";
            part.ObjectType = "well";
            part.SupportType = "door";
            part.IsIndoor = true;
            part.Material = "stone";
            part.Size = new PhysicalSize
            {
                W = new PhysicalDimension
                {
                    Value = 21,
                    Unit = "cm"
                },
                H = new PhysicalDimension
                {
                    Value = 29.70f,
                    Unit = "cm"
                }
            };
            part.LastViewed = new DateTime(2021, 12, 21);

            List<DataPin> pins = part.GetDataPins(null).ToList();
            Assert.Equal(9, pins.Count);

            DataPin? pin = pins.Find(
                p => p.Name == "original-fn" && p.Value == "house");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "current-fn" && p.Value == "street");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "object-type" && p.Value == "well");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "support-type" && p.Value == "door");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "indoor" && p.Value == "1");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "material" && p.Value == "stone");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "width" && p.Value == "21.00");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "height" && p.Value == "29.70");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);

            pin = pins.Find(p => p.Name == "last-viewed" && p.Value == "2021-12-21");
            Assert.NotNull(pin);
            TestHelper.AssertPinIds(part, pin!);
        }
    }
}
