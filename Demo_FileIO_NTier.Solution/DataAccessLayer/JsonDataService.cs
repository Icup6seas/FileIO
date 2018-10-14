using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Demo_FileIO_NTier.Models;
using Newtonsoft;
using Newtonsoft.Json;
using Demo_FileIO_NTier.DataAccessLayer;
using System.Threading.Tasks;

namespace Demo_FileIO_NTier.DataAccessLayer
{
    public class JsonDataService : IDataService
    {
        private string _dataFilePath;

        //
        // Read the json File and load a list of character objects
        public IEnumerable<Character> ReadAll()
        {
            List<Character> characters = new List<Character>();

            try
            {
                using (StreamReader sr = new StreamReader(_dataFilePath))
                {
                    string jsonString = sr.ReadToEnd();

                    Character characterList = JsonConvert.DeserializeObject<RootObject>(jsonString).Characters;

                    characters = characterList.Character;
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return characters;
        }

        //
        //Write current list of characters to the json data file

        public void WriteAll(IEnumerable<Character> characters)
        {
            RootObject rootObject = new RootObject();
            rootObject.Characters = new Characters();
            rootObject.Characters.Characters = characters as List<Character>;

            string jsonString = JsonConverter.SerializeObject(rootObject);

            try
            {
                StreamWriter writer = new StreamWriter(_dataFilePath);
                using (writer)
                {
                    writer.WriteLine(jsonString);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public JsonDataService()
        {
            _dataFilePath = DataSettings.dataFilePath;
        }

    }
}
