public class Controller
{
    public static Random random = new Random();

    //Timer
    public float GameTimer { get; private set; } = 0;

    public static List<Entity> entities = new();
    public static List<Item> itemObjs = new();
    public static HashSet<Item> items = new();

    public static Func<Item>[] itemMaker = 
    {
        () => new Heart(newItemPos)
    };

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

    public static Vector2 newItemPos = new();
    public static void SpawnItem(Vector2 pos)
    {
        newItemPos = pos;
        int randomIndex = 0;
        itemObjs.Add(itemMaker[randomIndex]());
    }
}
