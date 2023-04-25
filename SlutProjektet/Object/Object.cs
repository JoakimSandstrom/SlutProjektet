public class Object
{
    //Variables
    protected int scale = Map.scale;
    public Rectangle Rect {get; protected set;}
    protected Vector2 pos;
    protected Texture2D texture;

    //Dictionary of object textures
    protected static Dictionary<string,Texture2D> objectTextures = new();

    public virtual void Draw()
    {
        //Raylib.DrawRectangleRec(Rect, Color.DARKBLUE);
        Raylib.DrawTextureEx(texture,pos,0,scale,Color.WHITE);
    }
}
