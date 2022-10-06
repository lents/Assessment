using Assessment.Core;

namespace Assessment.Test
{
    public class TestletTest
    {
        [Fact]
        public void Should_Have_First_Two_Items_Pretest()
        {
            var testlet = CreateTestletWithTenItems();

            var items = testlet.Randomize();

            Assert.All(items.Take(2), i => { Assert.True(i.ItemType == ItemTypeEnum.Pretest); });
        }

        [Fact]
        public void Should_Have_All_Items_Random()
        {
            var testlet = CreateTestletWithTenItems();

            var items = testlet.Randomize();
            var randomizedItems = testlet.Randomize();

            Assert.All(items, item => {
                var itemIndex = items.IndexOf(item);
                var itemRandomizedIndex = randomizedItems.IndexOf(item);

                for (int i = 0; i < 10000; i++)
                {
                    if (itemIndex != itemRandomizedIndex)
                    {
                        break;
                    }
                    randomizedItems = testlet.Randomize();
                    itemRandomizedIndex = randomizedItems.IndexOf(item);
                }
                Assert.NotEqual(itemIndex, itemRandomizedIndex);
            });
        }

        private Testlet CreateTestletWithTenItems()
        {
            return new Testlet(Guid.NewGuid().ToString(),
                new List<Item>
                {
                    new Item{ItemId = "Operational_1", ItemType = ItemTypeEnum.Operational},
                    new Item{ItemId = "Operational_2", ItemType = ItemTypeEnum.Operational},
                    new Item{ItemId = "Operational_3", ItemType = ItemTypeEnum.Operational},
                    new Item{ItemId = "Operational_4", ItemType = ItemTypeEnum.Operational},
                    new Item{ItemId = "Operational_5", ItemType = ItemTypeEnum.Operational},
                    new Item{ItemId = "Operational_6", ItemType = ItemTypeEnum.Operational},
                    new Item{ItemId = "Pretest_1", ItemType = ItemTypeEnum.Pretest},
                    new Item{ItemId = "Pretest_2", ItemType = ItemTypeEnum.Pretest},
                    new Item{ItemId = "Pretest_3", ItemType = ItemTypeEnum.Pretest},
                    new Item{ItemId = "Pretest_4", ItemType = ItemTypeEnum.Pretest}
                });
        }
    }
}