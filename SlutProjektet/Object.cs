public class Object
{
    protected int scale = Map.scale;
    public Rectangle Rect {get; protected set;}
    protected Vector2 pos;
    protected static Texture2D texture;

    public virtual void Draw()
    {
        Raylib.DrawRectangleRec(Rect, Color.DARKBLUE);
        Raylib.DrawTextureEx(texture,pos,0,scale,Color.WHITE);
    }
}
