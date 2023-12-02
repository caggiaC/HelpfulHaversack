using static HelpfulHaversack.Services.ContainerAPI.Models.ItemTemplate;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class NullItemTemplate : IItemTemplate
    {
        private static readonly IItemTemplate _instance = new NullItemTemplate();

        public string Name { get { return String.Empty; } }
        public string Description { get { return String.Empty; } set { return; } }
        public double Weight { get { return 0; } set { return; } }
        public double Value { get { return 0; } set { return; } }
        public ItemRarity Rarity { get { return ItemRarity.COMMON; } set { return; } }
        public ItemType Type { get { return ItemType.OTHER; } set { return; } }

        public static IItemTemplate Instance { get { return _instance; } }

        private NullItemTemplate() { }

        public IItem CreateItemFrom()
        {
            return NullItem.Instance;
        }

        public bool IsNull() { return true; }





    }
}
