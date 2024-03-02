//ICI tous les services qui vont call les methodes de DAL  
//qui vont etres utilises par lapp
//a faire: save ,load ,modfiy(immuable?)
//
using Newtonsoft.Json;
using EmuMate.DAL;
using EmuMate.Models;

namespace EmuMate.Services
{
    public class AppServices
    {
        private static Dictionary<string, List<string>> data;
        private static string filePath;
        //METHODES PUBLIQUES
        public static void loadData()
        {
            //constructor : makes the file path and then calls init
            filePath = "data_Backup_Of_23-12-17_18_29_59";
            InitializeData(filePath);
        }
        public static void SaveChanges()
        {
            //make new data from all lists of objects
            //pack all data
            //EXEMPLE DATA
            // Dictionary<string, List<string>> packedData = new Dictionary<string, List<string>>
            // {
            //     { "Activite", new List<string> { "Value11", "Value12", "Value13" } },
            //     { "Appreciation", new List<string> { "Value21", "Value22" } },
            //     { "Cote", new List<string> { "Value31", "Value32", "Value33", "Value34" } }
            // };
            Dictionary<string, List<string>> packedData = packAll();
            JsonDataAccess.WriteDictionaryToFile(JsonDataAccess.GenerateBackupName(),packedData);
        }
        //METHODES PRIVEES
        private static void InitializeData(string filePath)
        {
            //gets data from json file, creates all lists then unpacks the dictionary to create instances of objects
            data = JsonDataAccess.ReadDictionaryFromFile(filePath);
            unpackAll(data);
        }
        //packer and unpackers
        private static Dictionary<string, List<string>> packAll()
        {
            List<string> dicoKeys = new List<string>{"Game","Emulator"};
            Dictionary<string, List<string>> output = new Dictionary<string, List<string>>{};
            foreach (string keyName in dicoKeys)
            {
                switch (keyName)
                {
                    case "Game":
                        List<string> packedObjectListEtudiant = new List<string>{};
                        foreach (object packableItem in Game.ListGame)
                        {
                            packedObjectListEtudiant.Add(pack(packableItem));
                        }
                        output[keyName] = packedObjectListEtudiant;
                        break;

                    case "Emulator":
                        List<string> packedObjectListEnseignant = new List<string>{};
                        foreach (object packableItem in Emulator.ListEmulator)
                        {
                            packedObjectListEnseignant.Add(pack(packableItem));
                        }
                        output[keyName] = packedObjectListEnseignant;
                        break;

                    default:
                        Console.WriteLine("how did we get here?");
                        break;
                }
            }
            return output;
        } 
        private static string pack(object item)
        {
            //take any object and make it into a dictionary
            //using the newton soft soft lib
            string output = JsonConvert.SerializeObject(item);
            return output;
        }
        private static void unpackAll(Dictionary<string, List<string>> data)
        {
            //call all unpackers with a switch case and consequently generate alll objects
            // unpacking order MUST BE :  Enseignant => Activite => Etudiants   => Evaluations

            //for the appreciations and cotes maybe add attribute id that represents from which student the eval is so that you can associate it with after

            //enseignant
            // Check if the key exists in the dictionary
            if (data.ContainsKey("Game"))
            {
                // Retrieve the list associated with the key
                List<string> gameList = data["Game"];

                // Iterate through the items in the list
                foreach (string item in gameList)
                {
                    Console.WriteLine($"created {unpackGame(item).ToString()} object");
                }
            }
            else
            {
                Console.WriteLine($"Key 'Game' not found in the dictionary.");
            }

            //Activite
            if (data.ContainsKey("Emulator"))
            {
                // Retrieve the list associated with the key
                List<string> emulatorList = data["Emulator"];

                // Iterate through the items in the list
                foreach (string item in emulatorList)
                {
                    Console.WriteLine($"created {unpackEmulator(item).ToString()} object");
                }
            }
            else
            {
                Console.WriteLine($"Key 'Enseignant' not found in the dictionary.");
            }
   
        }
        
        private static Game unpackGame(string packedItem)
        {
            //unpack object
            Game game = JsonConvert.DeserializeObject<Game>(packedItem);
            return game;
        }
        private static Emulator unpackEmulator(string packedItem)
        {
            //unpack object

            Emulator emulator = JsonConvert.DeserializeObject<Emulator>(packedItem);
            return emulator;
        }

    }
}
