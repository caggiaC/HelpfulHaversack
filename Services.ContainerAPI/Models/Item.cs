using HelpfulHaversack.Services.ContainerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace Services.ContainerAPI.Models
{
    public class Item : ItemTemplate
    {
        private readonly Guid _id;

        [Key]
        public Guid ItemId { get { return _id; } }

        public Item()
        {
            _id = Guid.NewGuid();
        }

        public Item(Guid guid)
        {
            _id = guid;
        }


    }
}
