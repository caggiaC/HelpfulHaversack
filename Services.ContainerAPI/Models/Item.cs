using HelpfulHaversack.Services.ContainerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace Services.ContainerAPI.Models
{
    public class Item : ItemTemplate, IItem
    {
        private readonly Guid _id;

        [Key]
        public Guid ItemId { get { return _id; } }

        public string DisplayName { get; set; }

        public Item(string name) : base(name)
        {
            _id = Guid.NewGuid();
            DisplayName = name;
        }

        public Item(string name, Guid guid) : base(name) 
        {
            _id = guid;
            DisplayName = name;
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
