public class Controller
{
    Random random = new Random();

    //Timer
    public float GameTimer { get; private set; } = 0;

    //Keeps track of runtime and spawns enemy every 10 seconds
    public void GameTime(List<Enemy> enemies)
    {
        GameTimer += Raylib.GetFrameTime();
        //Spawn new enemies every 10 seconds at a random place
        if (GameTimer > 10f)
        {
            enemies.Add(new Enemy((float)(random.NextDouble()*768)+96,(float)(random.NextDouble()*720)+144));
            GameTimer = 0;
        }
    }
}
