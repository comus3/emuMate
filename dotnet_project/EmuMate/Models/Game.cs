namespace EmuMate.Models
{
    public class Game
    {
        private string name;
        private string uid;
        private string picturePath;
        private static List<Game> listGame = new List<Game>();
        private string executionPath;

        public Game(string name, string picturePath, string emulatorUid, string uid = "uninitiated")
        {
            if (uid == "uninitiated")
            {
                uid = GenerateNewUid();
            }
            this.name = name;
            this.uid = uid;
            this.picturePath = picturePath;
            this.executionPath = emulatorUid;
            listGame.Add(this);
        }

        public static Game findEmulator(string UID)
        {
            Guid myGuid;
            if (Guid.TryParse(UID, out myGuid))
            {
                if (myGuid == Guid.Empty)
                {
                    return new Game("/", "/", "/", "/");
                }
            }
            foreach (Game game in listGame)
            {
                if (game.Uid == UID)
                {
                    return game;
                }
            }
            return new Game("/", "/", "/", "/");
        }
        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public static List<Game> ListEmulator
        {
            get { return listGame; }
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
                isUnique = !listGame.Any(obj => obj.Uid == newUid);

            } while (!isUnique);

            return newUid;
        }
    }
}