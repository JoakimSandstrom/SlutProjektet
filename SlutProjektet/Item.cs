public class Item
{
    public bool PickedUp { get; protected set; } = false;
    
    public Rectangle rect;

    //static variables
    protected static int scale = Map.scale;
    protected static Texture2D texture;
    public static string Rarity {get; protected set;} = "";
    protected static int id;
    protected int amount = 0;

    public virtual void PickUp()
    {
        
    }
    public virtual void Draw()
    {
        //Raylib.DrawRectangleRec(rect, Color.DARKBLUE);
        Raylib.DrawTextureEx(texture,new Vector2(rect.x,rect.y),0,scale,Color.WHITE);
    }
}
