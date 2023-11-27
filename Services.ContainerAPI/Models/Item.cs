using HelpfulHaversack.Services.ContainerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace Services.ContainerAPI.Models
{
    public class Item : ItemTemplate
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
    }
}
