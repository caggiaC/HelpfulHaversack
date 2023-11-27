namespace HelpfulHaversack.Services.ContainerAPI.Models
{
    public interface IItem
    {
        public Guid ItemId { get; }
        public string DisplayName { get; set; }
  
        public Boolean IsNull();
    }
}
