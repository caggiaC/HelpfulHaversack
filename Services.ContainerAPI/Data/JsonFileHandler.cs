
using Newtonsoft.Json;

namespace HelpfulHaversack.Services.ContainerAPI.Data
{
    public static class JsonFileHandler
    {
        /// <summary>
        /// Takes a collection of objects anda file path and writes the objetcs to a file in JSON format.
        /// </summary>
        /// <typeparam name="T">The Type of object being written to the file.</typeparam>
        /// <param name="filePath">The path to the file to be written to.</param>
        /// <param name="entries">The collection of objects to be written to the file.</param>
        public static void WriteCollectionToFile<T>(string filePath, ICollection<T> entries)
        {
            using StreamWriter outputFile = new(filePath);
            foreach (T entry in entries)
                outputFile.WriteLine(JsonConvert.SerializeObject(entry));
        }

        /// <summary>
        /// Takes a file path and returns a list of objects of type T from the file.
        /// </summary>
        /// <typeparam name="T">The type of object to be returned.</typeparam>
        /// <param name="filePath">The path to the file to be read from.</param>
        /// <returns>A List of type T containing the contents of the file.</returns>
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
