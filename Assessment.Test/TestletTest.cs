using Assessment.Core;
using System.ComponentModel.DataAnnotations;

namespace Assessment.Test
{
    public class TestletTest
    {
        [Theory]
        [InlineData(3)]
        public void Should_Validate_Correct_Number_Pretest_Items(int numberOfPretest)
        {
            Assert.Throws<ValidationException>(() => { var testlet = CreateTestlet(numberOfPretest, Testlet.NUMBER_OF_OPERATIONAL_ITEMS); });
        }

        [Theory]
        [InlineData(5)]
        public void Should_Validate_Correct_Number_Operational_Items(int numberOfOperational)
        {
            Assert.Throws<ValidationException>(() => { var testlet = CreateTestlet(Testlet.NUMBER_OF_PRETEST_ITEMS, numberOfOperational); });
        }

        [Fact]
        public void Should_Have_First_Two_Items_Pretest()
        {
            var testlet = CreateTestlet();

            var items = testlet.Randomize();

            Assert.All(items.Take(2), i => { Assert.True(i.ItemType == ItemTypeEnum.Pretest); });
        }

        [Fact]
        public void Should_Have_All_Items_Random()
        {
            var testlet = CreateTestlet();

            var items = testlet.Randomize();
            var randomizedItems = testlet.Randomize();
            
            Assert.NotEqual(items, randomizedItems);           
        }

        private Testlet CreateTestlet(int numberOfPretestItems = Testlet.NUMBER_OF_PRETEST_ITEMS, int numberOfOperationalItems = Testlet.NUMBER_OF_OPERATIONAL_ITEMS)
        {
            var items = new List<Item>();
            for (int i = 0; i < numberOfOperationalItems; i++)
            {
                items.Add(new Item { ItemId = i.ToString(), ItemType = ItemTypeEnum.Operational });
            }
            for (int i = 0; i < numberOfPretestItems; i++)
            {
                items.Add(new Item { ItemId = i.ToString(), ItemType = ItemTypeEnum.Pretest });
            }
            return new Testlet(Guid.NewGuid().ToString(), items);
        }
    }
}