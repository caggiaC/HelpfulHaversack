
using HelpfulHaversack.Services.ContainerAPI.Models;


namespace HelpfulHaversack.Services.ContainerAPI.Data
{
    /// <summary>
    /// A Singleton class that represents a set of all ItemTemplates based on their unique names.
    /// </summary>
    public sealed class ItemTemplateSet
    {
        private static readonly Dictionary<string,ItemTemplate> _templates = new();
        private static readonly Lazy<ItemTemplateSet> _instance = new(() => new ItemTemplateSet());

        private ItemTemplateSet()
        {
            //Seed list; temporary for development
            //SeedList();
            //WriteToFile("./Data/");

            //Load template list from file
            LoadFromFile("./Data/");

        }//End Constructor

        /// <summary>
        /// The singleton instance of ItemTemplateSet.
        /// </summary>
        public static ItemTemplateSet Instance { get { return _instance.Value; } }

        /// <summary>
        /// Adds a new ItemTemplate to the set.
        /// </summary>
        /// <param name="template">The new ItemTemplate.</param>
        /// <exception cref="ArgumentException">
        /// An ItemTemplate with the same name (case sensitve) already
        /// exists within the set.
        /// </exception>
        public void Add(ItemTemplate template)
        {       
            if (_templates.ContainsKey(template.Name))
                throw new ArgumentException("Template with this name already exists.");

            _templates.Add(template.Name, template);
        }

        /// <summary>
        /// Gets an ItemTemplate from the set by name (case sensitive).
        /// </summary>
        /// <param name="name">The name of the requested ItemTemplate.</param>
        /// <returns>The ItemTemplate with the macthing name, or null if one was not found.</returns>
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

        /// <summary>
        /// Gets a list of all currently existing ItemTemplates in the set.
        /// </summary>
        /// <returns>A List of ItemTemplates.</ItemTemplate></returns>
        public List<ItemTemplate> GetAllTemplates()
        {
            return new List<ItemTemplate>(_templates.Values);
        }

        /// <summary>
        /// Deletes an ItemTemplate from the set based on the ItemTemplate name (case sensitive).
        /// </summary>
        /// <param name="name">The name of the ItemTemplate to be deleted.</param>
        /// <exception cref="ArgumentException">No template exists with the name that was passed.</exception>
        public void RemoveTemplate(string name) 
        {
            if (!_templates.ContainsKey(name))
                throw new ArgumentException($"Template with the name \"{name}\" was not found.");

            _templates.Remove(name);
        }

        /// <summary>
        /// Replaces an ItemTemplate in the set with a new ItemTemplate with the same name (case sensitve).
        /// </summary>
        /// <param name="template">The new ItemTemplate</param>
        /// <exception cref="ArgumentException">
        /// The passed ItemTemplate has a name that does not exactly match an ItemTemplate that currently exists
        /// within the set.
        /// </exception>
        public void UpdateTemplate(ItemTemplate template)
        {
            if (!_templates.ContainsKey(template.Name))
                throw new ArgumentException($"{template.Name} does not exist.");

            _templates[template.Name] = template;
        }

        /// <summary>
        /// For development purposes. Seeds the set with four ItemTemplates.
        /// </summary>
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

        /// <summary>
        /// Writes the current set of ItemTemplates to a file.
        /// </summary>
        public void Save()
        {
            WriteToFile("./Data/");
        }

        /// <summary>
        /// Writes the current set of ItemTemplates to a "Templates.txt" file in the specified location.
        /// </summary>
        /// <param name="path">A path to the directory the file is to be written to.</param>
        private static void WriteToFile(string path)
        {
            JsonFileHandler.WriteCollectionToFile(Path.Combine(path, "Templates.txt"), _templates.Values);
        }

        /// <summary>
        /// Loads a set of ItemTemplates from the "Templates.txt" in the specified location.
        /// </summary>
        /// <param name="path">A path to the directory containing the "Templates.txt" file.</param>
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
