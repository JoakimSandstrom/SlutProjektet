public class Map
{
    //World scale. All textures are scaled by this.
    public static int scale = 3;

    //Load texture to memory
    private Texture2D tx = Raylib.LoadTexture("Sprites/Map.png");

    //List of collision rectangles
    public static List<Rectangle> collision = new();

    //Add collision rectangles
    public Map()
    {
        collision.Add(new Rectangle(0,0,tx.width*scale,144));
        collision.Add(new Rectangle(0,0,96,tx.height*scale));
        collision.Add(new Rectangle(tx.width*scale-96,0,96,tx.height*scale));
        collision.Add(new Rectangle(0,tx.height*scale-96,tx.width*scale,144));
        collision.Add(new Rectangle(240,240,48,48));
        collision.Add(new Rectangle(336,192,48,48));
        collision.Add(new Rectangle(576,192,48,48));
        collision.Add(new Rectangle(672,240,48,48));
        collision.Add(new Rectangle(192,720,48,48));
        collision.Add(new Rectangle(720,720,48,48));
    }

    //Draw Texture on screen
    public void Draw()
    {
        Raylib.DrawTextureEx(tx, Vector2.Zero, 0, scale, Color.WHITE);
        //foreach (Rectangle r in collision)
        //{
        //    Raylib.DrawRectangleRec(r, Color.DARKBLUE);
        //}
    }
}
