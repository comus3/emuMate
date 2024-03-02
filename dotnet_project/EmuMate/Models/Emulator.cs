namespace EmuMate.Models
{
    public class Emulator
    {
        private string name;
        private string uid;
        private string picturePath;
        private static List<Emulator> listEmulator = new List<Emulator>();
        private string executionPath;

        public Emulator(string name,string picturePath, string executionPath, string uid = "uninitiated")
        {
            if (uid == "uninitiated")
            {
                uid = GenerateNewUid();
            }
            this.name = name;
            this.uid = uid;
            this.picturePath = picturePath;
            this.executionPath = executionPath;
            listEmulator.Add(this);
        }

        public static Emulator findEmulator(string UID)
        {
            Guid myGuid;
            if (Guid.TryParse(UID, out myGuid))
            {
                if (myGuid == Guid.Empty)
                {
                    return new Emulator("/","/","/","/");
                }
            }
            foreach (Emulator emulator in listEmulator)
            {
                if (emulator.Uid == UID)
                {
                    return emulator;
                }
            }
            return new Emulator("/","/","/", "/");
        }
        public string Uid 
        {
            get{return uid;}
            set{uid = value;}
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static List<Emulator> ListEmulator
        {
            get { return listEmulator; }
        }
        private static string GenerateNewUid()
        {
            string newUid;
            bool isUnique;

            do
            {
                // Generate a new UID
                newUid = Guid.NewGuid().ToString();

                // Check if the UID is unique
                isUnique = !listEmulator.Any(obj => obj.Uid == newUid);

            } while (!isUnique);

            return newUid;
        }
    }
}