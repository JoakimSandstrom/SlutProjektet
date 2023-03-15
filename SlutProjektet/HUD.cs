public class HUD
{
    private float pHealth;
    private Vector2 pos;
    private Texture2D tx = Raylib.LoadTexture("Sprites/Map.png");
    public HUD()
    {
        
    }
    public void Update(Player p)
    {
        pHealth = p.Health;
        while(pHealth>0)
        {

        }
    }
    public void Draw()
    {
        Raylib.DrawTextureEx(tx, Vector2.Zero, 0, Map.scale, Color.WHITE);
    }
}
