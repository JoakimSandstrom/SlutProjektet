public class Controller
{
    Random random = new Random();

    //Timer
    public float GameTimer { get; private set; } = 0;

    //Create 1 Player and Enemy when constructed
    public Controller(List<Entity> entities)
    {
        entities.Add(new Player());
        entities.Add(new Enemy((float)(random.NextDouble()*768)+96,(float)(random.NextDouble()*720)+144, entities));
    }

    //Keeps track of runtime and spawns enemy every 10 seconds
    public void GameTime(List<Entity> entities)
    {
        GameTimer += Raylib.GetFrameTime();
        //Spawn new enemies every 10 seconds at a random place
        if (GameTimer > 10f)
        {
            entities.Add(new Enemy((float)(random.NextDouble()*768)+96,(float)(random.NextDouble()*720)+144, entities));
            GameTimer = 0;
        }
    }
}
