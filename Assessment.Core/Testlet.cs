using System.Runtime.ConstrainedExecution;

namespace Assessment.Core
{
    public class Testlet
    {
        public string TestletId;
        private List<Item> Items;
        public Testlet(string testletId, List<Item> items)
        {
            TestletId = testletId;
            Items = items;
        }
        public List<Item> Randomize()
        {
            var random = new Random();
            Items = Items.OrderBy(item => random.Next()).ToList();

            var pretestItems = Items.Where(i => i.ItemType == ItemTypeEnum.Pretest).ToList();
            var randomizedPretestItems = pretestItems.OrderBy(item => random.Next()).Take(2);            

            foreach (var pretestItem in randomizedPretestItems)
            {
                Items.Remove(pretestItem);
                Items.Insert(0, pretestItem);                
            }            
            return Items;
        }
    }
}