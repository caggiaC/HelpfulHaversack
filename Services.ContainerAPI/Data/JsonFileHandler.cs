
using Newtonsoft.Json;

namespace HelpfulHaversack.Services.ContainerAPI.Data
{
    public static class JsonFileHandler
    {
        public static void WriteCollectionToFile<T>(string filePath, ICollection<T> entries)
        {
            using StreamWriter outputFile = new(filePath);
            foreach (T entry in entries)
                outputFile.WriteLine(JsonConvert.SerializeObject(entry));
        }

        public static List<T> GetFileContents<T>(string filePath)
        {
            List<T> deseralizedValues = new();

            using StreamReader sourceFile = new(filePath);
            while(!sourceFile.EndOfStream)
            {
                string? currentLine = sourceFile.ReadLine();

                if (currentLine == null) continue;

                T? current = JsonConvert.DeserializeObject<T>(currentLine);

                if(current != null)
                    deseralizedValues.Add(current);
            }

            return deseralizedValues;
        }
    }
}
