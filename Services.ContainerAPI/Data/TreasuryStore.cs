using HelpfulHaversack.Services.ContainerAPI.Data;
using HelpfulHaversack.Services.ContainerAPI.Models;
using Services.ContainerAPI.Models;

namespace Services.ContainerAPI.Data
{
    /// <summary>
    /// A singleton class that represents a set of Treasury objects which are unique by their TreasuryId.
    /// </summary>
    public sealed class TreasuryStore
    {
        private static readonly List<Treasury> _treasuries = new();
        private readonly ItemTemplateSet _templates = ItemTemplateSet.Instance;

        private static readonly Lazy<TreasuryStore> _instance = new(() => new TreasuryStore());

        private TreasuryStore()
        {
            //Seed list; temporary for development
            //SeedList();
            //WriteToFile("./Data/");

            //Load treasuries from file
            ReadFromFile("./Data/");
        }

        /// <summary>
        /// The singleton instance of the TreasuryStore class.
        /// </summary>
        public static TreasuryStore Instance { get { return _instance.Value; } }

        /// <summary>
        /// Returns the Treasury from the set with the matching Guid.
        /// </summary>
        /// <param name="id">The Guid of the Treasury to be retreived.</param>
        /// <returns>The Treasury with the matching Guid, or null if one is not found.</returns>
        public Treasury? GetTreasury(Guid id)
        {
            return _treasuries.FirstOrDefault(t => t.TreasuryId == id);
        }

        /// <summary>
        /// Gets a List of all treasuries in the set.
        /// </summary>
        /// <returns>A List of type Treasury containing every Treasury in the set.</returns>
        public List<Treasury> GetAllTreasuries() { return _treasuries; }


        /// <summary>
        /// Gets all Treasuries in the set that have a name containing the passed string (case insensitive).
        /// </summary>
        /// <param name="treasuryName">The </param>
        /// <returns></returns>
        public List<Treasury> GetTreasuriesByName(string treasuryName)
        {
            return _treasuries.FindAll(t => t.Name.ToLower().Contains(treasuryName.ToLower()));
        }

        /// <summary>
        /// Adds a Treasury to the set.
        /// </summary>
        /// <param name="treasury">The Treasury to add to the set.</param>
        /// <exception cref="ArgumentException">A Treasury with the same Guid already exists within the set.</exception>
        public void AddTreasury(Treasury treasury)
        {
            if(_treasuries.Contains(treasury)) { throw new ArgumentException("Treasury with this ID already exists."); }
            _treasuries.Add(treasury);
        }

        /// <summary>
        /// Removes a Treasury from the set.
        /// </summary>
        /// <param name="treasuryId">The Guid of the Treasury to remove.</param>
        public void RemoveTreasury(Guid treasuryId)
        {
            _treasuries.Remove(_treasuries.First(x => x.TreasuryId == treasuryId));
        }

        /// <summary>
        /// Removes a Treasury from the set.
        /// </summary>
        /// <param name="treasury">The Treasury to remove from the set.</param>
        public void RemoveTreasury(Treasury treasury)
        {
            _treasuries.Remove(treasury);
        }

        /// <summary>
        /// Removes an Item from a Treasury in the set.
        /// </summary>
        /// <param name="treasuryId">The Guid of the Treasury to remove the item from.</param>
        /// <param name="itemId">The Guid of the Item to remove.</param>
        public void RemoveItemFromTreasury(Guid treasuryId, Guid itemId)
        {
            _treasuries.First(t => t.TreasuryId == treasuryId).RemoveItem(itemId);
        }

        /// <summary>
        /// Replaces a Treasury in the set with a new one passed in with the same Guid.
        /// </summary>
        /// <param name="treasury">The updated Treasury.</param>
        /// <exception cref="ArgumentException">
        /// No Treasury in the set has a Guid that matches that of the Treasury that was passed in.
        /// </exception>
        public void UpdateTreasury(Treasury treasury)
        {
            if(!_treasuries.Contains(treasury))
                throw new ArgumentException($"Treasury with id {treasury.TreasuryId} does not exist.");

            _treasuries[_treasuries.IndexOf(treasury)] = treasury;
        }

        /// <summary>
        /// Checks if a Treasury in the set has a Guid matches the that of the passed in Treasury.
        /// </summary>
        /// <param name="treasury">The Treasury to search for.</param>
        /// <returns>True if a matching Treasury was found, false otherwise.</returns>
        public bool Contains(Treasury treasury)
        {
            foreach(Treasury t in _treasuries)
                if(t.TreasuryId == treasury.TreasuryId) return true;

            return false;
        }

        /// <summary>
        /// Checks if a Treasury in the set has a matching Guid
        /// </summary>
        /// <param name="treasuryId">The Guid to search for.</param>
        /// <returns></returns>
        public bool Contains(Guid treasuryId)
        {
            foreach(Treasury t in _treasuries)
                if(t.TreasuryId == treasuryId) return true;

            return false;
        }

        /// <summary>
        /// Returns a list of TreasuryReference objects for each Treasury in the set.
        /// </summary>
        /// <returns>A List of TreasuryReference objects.</returns>
        public List<TreasuryReference> GetTreasuryReferences()
        {
            List<TreasuryReference> referenceList = new();

            foreach (Treasury treasury in _treasuries)
            {
                referenceList.Add(TreasuryReference.CreateReference(treasury));
            }

            return referenceList;
        }

        /// <summary>
        /// Writes the current set to a file
        /// </summary>
        public void Save()
        {
            WriteToFile("./Data/");
        }

        /// <summary>
        /// For development purposes. Seeds the set with two treasuries.
        /// </summary>
        private void SeedList()
        {
            Treasury temp = new()
            {
                Name = "Mysterious Chest",
                GP = 100,
                SP = 200,
                CP = 500
            };

            ItemTemplate? TEMPlate = _templates.GetTemplate("Dagger");
            if (TEMPlate != null) temp.AddItem(TEMPlate.CreateItemFrom());

            TEMPlate = _templates.GetTemplate("Dagger");
            if (TEMPlate != null) temp.AddItem(TEMPlate.CreateItemFrom());

            TEMPlate = _templates.GetTemplate("Necklace");
            if (TEMPlate != null) temp.AddItem(TEMPlate.CreateItemFrom());

            _treasuries.Add(temp);


            temp = new()
            {
                Name = "Bag of Holding",
            };

            TEMPlate = _templates.GetTemplate("Longsword");
            if (TEMPlate != null) temp.AddItem(TEMPlate.CreateItemFrom());

            _treasuries.Add(temp);
        }

        /// <summary>
        /// Writes the current state of the set to a file by invoking the JsonFileHandler class.
        /// </summary>
        /// <param name="path">The path to the directory to write the "Treasuries.txt" file to.</param>
        private static void WriteToFile(string path)
        {
            JsonFileHandler.WriteCollectionToFile(Path.Combine(path, "Treasuries.txt"), _treasuries);
        }

        /// <summary>
        /// Loads the set with data from a file by invoking the JsonFileHandler class.
        /// </summary>
        /// <param name="path">The path to the "Treasuries.txt" file to load data from.</param>
        private static void ReadFromFile(string path)
        {
            foreach(Treasury treasury in 
                JsonFileHandler.GetFileContents<Treasury>(Path.Combine(path, "Treasuries.txt")))
            {
                _treasuries.Add(treasury);
            }
        }
    }
}
