public class Controller
{
    //Public Static Random to generate random numbers
    public static Random random = new Random();

    //Timer (public get, will be accessed by the HUD)
    public float GameTimer { get; private set; } = 0;

    //Public Static Lists that multiple classes need access to
    public static List<Entity> entities = new();
    public static List<ItemPickup> itemPickups = new();

    //Public Static variable to easily find the player in the entities list
    public static int playerIndex;

    //Create 1 Player and Enemy when constructed
    public Controller()
    {
        entities.Add(new Player());
        playerIndex = 0;
        entities.Add(new Slime((float)(random.NextDouble()*768)+96,(float)(random.NextDouble()*720)+144));
    }

    //Keeps track of runtime and spawns enemy every 10 seconds
    public void GameTime()
    {
        GameTimer += Raylib.GetFrameTime();
        //Spawn new enemies every 10 seconds at a random place
        if (GameTimer > 10f)
        {
            entities.Add(new SmallOrc((float)(random.NextDouble()*768)+96,(float)(random.NextDouble()*720)+144));
            GameTimer = 0;
        }
    }
    public static void SpawnItem(Vector2 pos, int itemId)
    {
        itemPickups.Add(new ItemPickup(pos,itemId));
    }
}
