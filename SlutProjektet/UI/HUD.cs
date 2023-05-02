public class HUD : UI
{
    //Static variables
    private static float hpFactor = 1;
    private static float mpFactor = 1;
    private static Vector2 HPpos = new Vector2(40,40);
    private static Vector2 enemyQueuePos = new Vector2(800,40);
    //Load textures once
    private static Texture2D hpBottom= Raylib.LoadTexture("Sprites/HPBottom.png");
    private static Texture2D hp1 = Raylib.LoadTexture("Sprites/HP1.png");
    private static Texture2D hp2 = Raylib.LoadTexture("Sprites/HP2.png");
    private static Texture2D mp1 = Raylib.LoadTexture("Sprites/MP1.png");
    private static Texture2D mp2 = Raylib.LoadTexture("Sprites/MP2.png");
    private static Texture2D hpTop = Raylib.LoadTexture("Sprites/HPTop.png");
    private static Texture2D nextEnemyText = Raylib.LoadTexture("Sprites/NextEnemy.png");
    private static Texture2D enemyQueue = Raylib.LoadTexture("Sprites/EnemyQueue.png");
    private static Texture2D slime = Raylib.LoadTexture("Sprites/Slime.png");
    private static Texture2D smallOrc = Raylib.LoadTexture("Sprites/SmallOrc.png");
    
    public static void Update(Player p)
    {
        //Finds out how much the healthbar needs to be squished
        if (p.Health > 0 ) hpFactor = (float)p.Health/(float)p.BaseHealth;
        else hpFactor = 0;
    }
    public static void Draw()
    {
        //HP/MP Bar
        //Draw each layer on top of eachother
        Raylib.DrawTextureEx(hpBottom, HPpos, 0, 1, Color.WHITE);
        Raylib.DrawTextureEx(hp1, new Vector2(HPpos.X+6,HPpos.Y+12), 0, 1, Color.WHITE);
        //Change and reset width to show decrease in HP or MP
        hp2.width = (int)(hp2.width*hpFactor);
        Raylib.DrawTextureEx(hp2, new Vector2(HPpos.X+6,HPpos.Y+12), 0, 1, Color.WHITE);
        hp2.width = (int)(hp2.width/hpFactor);
        Raylib.DrawTextureEx(mp1, new Vector2(HPpos.X+6,HPpos.Y+42), 0, 1, Color.WHITE);
        mp2.width = (int)(mp2.width*mpFactor);
        Raylib.DrawTextureEx(mp2, new Vector2(HPpos.X+6,HPpos.Y+42), 0, 1, Color.WHITE);
        mp2.width = (int)(mp2.width/mpFactor);
        Raylib.DrawTextureEx(hpTop, HPpos, 0, 1, Color.WHITE);

        //Next Enemy
        Raylib.DrawTextureEx(nextEnemyText, enemyQueuePos, 0, 2, Color.WHITE);
        Raylib.DrawTextureEx(enemyQueue, new Vector2(enemyQueuePos.X+6,enemyQueuePos.Y+52), 0, 2, Color.WHITE);
        //Draw different enemies depending on the enemyqueue
        int i = 0;
        foreach(Entity e in Controller.enemyQueue)
        {
            if(e is Slime) Raylib.DrawTextureEx(slime, new Vector2(enemyQueuePos.X+25,enemyQueuePos.Y+52+(23+(i*84))), 0, 3, Color.WHITE);
            else Raylib.DrawTextureEx(smallOrc, new Vector2(enemyQueuePos.X+35,enemyQueuePos.Y+52+(29+(i*84))), 0, 3, Color.WHITE);
            i++;
        }
    }
}
