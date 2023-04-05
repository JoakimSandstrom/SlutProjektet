public class Object
{
    protected int scale = Map.scale;
    public Rectangle Rect {get; protected set;}
    protected Vector2 pos;
    protected Texture2D texture;
    protected static Dictionary<int,Texture2D> itemTextures = new();
    protected static Dictionary<string,Texture2D> objectTextures = new();

    public virtual void Draw()
    {
        Raylib.DrawRectangleRec(Rect, Color.DARKBLUE);
        Raylib.DrawTextureEx(texture,pos,0,scale,Color.WHITE);
    }
}
