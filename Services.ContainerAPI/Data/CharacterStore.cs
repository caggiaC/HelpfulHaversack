using HelpfulHaversack.Services.ContainerAPI.Data;
using HelpfulHaversack.Services.ContainerAPI.Models;
using Services.ContainerAPI.Models;

namespace HelpfulHaversack.Services.ContainerAPI.Data
{
    public sealed class CharacterStore
    { 
        private static readonly List<Character> _characters = new();

        private static readonly Lazy<CharacterStore> _instance = new(() => new CharacterStore());

        private CharacterStore()
        {
            //Seed list; temporary for development
            //SeedList();
            //WriteToFile("./Data/");

            //Load characters from file
            ReadFromFile("./Data/");
        }   

        public static CharacterStore Instance { get { return _instance.Value; } }

        public Character? GetCharacter(Guid id)
        {
            return _characters.FirstOrDefault(c => c.InventoryId == id);
        }

        public List<Character> GetAllCharacters() { return _characters; }

        public List<Character> GetCharactersByName(string characterName)
        {
            return _characters.FindAll(c => c.Name.ToLower().Contains(characterName.ToLower()));
        }

        public Character AddCharacter(Character character)
        {
            if (_characters.Contains(character)) { throw new ArgumentException("Character with this ID already exists."); }
            _characters.Add(character);
            return character;
        }   

        public void RemoveCharacter(Guid characterId)
        {
            _characters.Remove(_characters.First(x => x.CharacterId == characterId));
        }   

        public void RemoveCharacter(Character character)
        {
            _characters.Remove(character);
        }

        public void UpdateCharacter(Character character)
        {
            if (!_characters.Contains(character))
                throw new ArgumentException($"Character with id {character.CharacterId} does not exist.");

            _characters[_characters.IndexOf(character)] = character;
        }

        public void Save()
        {
            WriteToFile("./Data/");
        }

        private static void SeedList()
        {
            Character temp = new()
            {
                InventoryId =Guid.Empty,
                Xp = 0,
                Name = "Test Character",
                Class = "Fighter",
                MaxHP = 10,
                CurrentHp = 10,
                ArmorClass = 11,
                Str = 16,
                Dex = 12,
                Con = 14,
                Wis = 10,
                Int = 8,
                Cha = 8
            };

            _characters.Add(temp);
        }

        private static void WriteToFile(string path)
        {
            JsonFileHandler.WriteCollectionToFile(Path.Combine(path, "Characters.txt"), _characters);
        }

        private static void ReadFromFile(string path)
        {
            foreach (Character character in 
                JsonFileHandler.GetFileContents<Character>(Path.Combine(path, "Characters.txt")))
            {
                _characters.Add(character);
            }
        }

    }
}
