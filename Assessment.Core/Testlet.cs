using System.ComponentModel.DataAnnotations;

namespace Assessment.Core
{
    public class Testlet
    {
        public const int NUMBER_OF_PRETEST_ITEMS = 4;
        public const int NUMBER_OF_FIRST_PRETEST_ITEMS = 2;
        public const int NUMBER_OF_OPERATIONAL_ITEMS = 6;

        public string TestletId;
        private List<Item> Items;
        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
            ValidateItems();
        }        

        public List<Item> Randomize()
        {
            var random = new Random();
            Items = Items.OrderBy(item => random.Next()).ToList();

            var pretestItems = Items.Where(i => i.ItemType == ItemTypeEnum.Pretest).ToList();
            var randomizedPretestItems = pretestItems.OrderBy(item => random.Next()).Take(NUMBER_OF_FIRST_PRETEST_ITEMS);            

            foreach (var pretestItem in randomizedPretestItems)
            {
                Items.Remove(pretestItem);
                Items.Insert(0, pretestItem);                
            }            
            return Items;
        }

        private void ValidateItems()
        {
            var pretestItemsCount = Items.Count(i => i.ItemType == ItemTypeEnum.Pretest);
            if (pretestItemsCount != NUMBER_OF_PRETEST_ITEMS)
            {
                throw new ValidationException($"Testlet should have {NUMBER_OF_PRETEST_ITEMS} pretest items");
            }

            var operationalItemsCount = Items.Count(i => i.ItemType == ItemTypeEnum.Operational);
            if (operationalItemsCount != NUMBER_OF_OPERATIONAL_ITEMS)
            {
                throw new ValidationException($"Testlet should have {NUMBER_OF_OPERATIONAL_ITEMS} operational items");
            }

        }
    }
}