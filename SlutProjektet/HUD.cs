public class HUD : UI
{
    private static float hpFactor = 1;
    private static float mpFactor = 1;
    private static Vector2 pos = new Vector2(40,40);
    private static Texture2D hpBottom= Raylib.LoadTexture("Sprites/HPBottom.png");
    private static Texture2D hp1 = Raylib.LoadTexture("Sprites/HP1.png");
    private static Texture2D hp2 = Raylib.LoadTexture("Sprites/HP2.png");
    private static Texture2D mp1 = Raylib.LoadTexture("Sprites/MP1.png");
    private static Texture2D mp2 = Raylib.LoadTexture("Sprites/MP2.png");
    private static Texture2D hpTop = Raylib.LoadTexture("Sprites/HPTop.png");
    
    public static void Update(Player p)
    {
        hpFactor = (float)p.Health/(float)p.BaseHealth;
    }
    public static void Draw()
    {
        Raylib.DrawTextureEx(hpBottom, pos, 0, 1, Color.WHITE);
        Raylib.DrawTextureEx(hp1, new Vector2(pos.X+6,pos.Y+12), 0, 1, Color.WHITE);
        hp2.width = (int)(hp2.width*hpFactor);
        Raylib.DrawTextureEx(hp2, new Vector2(pos.X+6,pos.Y+12), 0, 1, Color.WHITE);
        hp2.width = (int)(hp2.width/hpFactor);
        Raylib.DrawTextureEx(mp1, new Vector2(pos.X+6,pos.Y+42), 0, 1, Color.WHITE);
        mp2.width = (int)(mp2.width*mpFactor);
        Raylib.DrawTextureEx(mp2, new Vector2(pos.X+6,pos.Y+42), 0, 1, Color.WHITE);
        mp2.width = (int)(mp2.width/mpFactor);
        Raylib.DrawTextureEx(hpTop, pos, 0, 1, Color.WHITE);
    }
}
