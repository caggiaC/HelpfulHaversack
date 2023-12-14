
using HelpfulHaversack.Services.ContainerAPI.Models;


namespace HelpfulHaversack.Services.ContainerAPI.Data
{
    public sealed class ItemTemplateMasterSet
    {
        private static readonly Dictionary<string,ItemTemplate> _templates = new();
        private static readonly Lazy<ItemTemplateMasterSet> _instance = new(() => new ItemTemplateMasterSet());

        private ItemTemplateMasterSet()
        {
            //Seed list; temporary for development
            //SeedList();
            //WriteToFile("./Data/");

            //Load template list from file
            LoadFromFile("./Data/");

        }//End Constructor

        public static ItemTemplateMasterSet Instance { get { return _instance.Value; } }

        public void Add(ItemTemplate template)
        {
            if (template == null)
                throw new ArgumentException("Null template was recieved.");
                
            if (_templates.ContainsKey(template.Name))
                throw new ArgumentException("Template with this name already exists.");

            _templates.Add(template.Name, template);
        }

        public ItemTemplate? GetTemplate(string name)
        {
            try
            {
                return _templates[name];
            }
            catch(KeyNotFoundException)
            {
                return null;
            }
            
        }

        public List<ItemTemplate> GetAllTemplates()
        {
            return new List<ItemTemplate>(_templates.Values);
        }

        public void RemoveTemplate(string name) 
        {
            if (!_templates.ContainsKey(name))
                throw new ArgumentException($"Template with the name \"{name}\" was not found.");

            _templates.Remove(name);
        }

        public void UpdateTemplate(ItemTemplate template)
        {
            if (template == null)
                throw new ArgumentException("Bad template recieved.");

            if (!_templates.ContainsKey(template.Name))
                throw new ArgumentException($"{template.Name} does not exist.");

            _templates[template.Name] = template;
        }

        private void SeedList()
        {
            Add(new ItemTemplate("Longsword")
            {
                Description = "A versatile melee weapon.",
                Weight = 3,
                Value = 15,
                Type = ItemTemplate.ItemType.WEAPON,
                Rarity = ItemTemplate.ItemRarity.COMMON
            });

            Add(new ItemTemplate("Dagger")
            {
                Description = "A simple melee weapon.",
                Weight = 1,
                Value = 2,
                Type = ItemTemplate.ItemType.WEAPON,
                Rarity = ItemTemplate.ItemRarity.COMMON
            });

            Add(new ItemTemplate("Shortbow")
            {
                Description = "A simple ranged weapon.",
                Weight = 2,
                Value = 25,
                Type = ItemTemplate.ItemType.WEAPON,
                Rarity = ItemTemplate.ItemRarity.COMMON
            });

            Add(new ItemTemplate("Necklace")
            {
                Description = "An ornate necklace.",
                Weight = 0.1,
                Value = 50,
                Type = ItemTemplate.ItemType.OTHER,
                Rarity = ItemTemplate.ItemRarity.COMMON
            });
        }

        public void Close()
        {
            WriteToFile("./Data/");
        }

        private static void WriteToFile(string path)
        {
            JsonFileHandler.WriteCollectionToFile(Path.Combine(path, "Templates.txt"), _templates.Values);
        }

        private static void LoadFromFile(string path)
        { 
            foreach (ItemTemplate template in
                JsonFileHandler.GetFileContents<ItemTemplate>(Path.Combine(path, "Templates.txt")))
            {
                _templates.Add(template.Name, template);
            }
        }
    }
}
