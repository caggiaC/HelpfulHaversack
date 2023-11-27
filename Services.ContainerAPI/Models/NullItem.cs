using System.ComponentModel;

namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public class NullItem : ItemTemplate, IItem
    {
        public static readonly IItem _instance = new NullItem();

        public Guid ItemId { get { return Guid.Empty; } }
        public string DisplayName { get { return String.Empty; } set { return; } }
        public new Boolean IsNull() { return true; }

        public static IItem Instance { get { return _instance; } }
        private NullItem() : base(String.Empty) { }
    }
}
