using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace HelpfulHaversack.Web.Models
{
    public class Item : ItemTemplate
    {
        [JsonProperty]
        private readonly Guid _id;

        [Key]
        public Guid ItemId { get { return _id; } }

        public new string Name { get; set; } = "A New Item";
        public string DisplayName { get; set; } = "A New Item";

        public Item() : base ("Item Instance")
        {
            _id = Guid.NewGuid();
        }

        [JsonConstructor]
        public Item(Guid ItemId) : base("Item Instance")
        {
            _id = ItemId;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            try { return ((Item)obj).ItemId == _id; }
            catch { return false; }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
