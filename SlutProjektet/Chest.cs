public class Chest : Interactive
{
    public Chest()
    {
        Rect = new Rectangle(400,400,16*scale,16*scale);
        texture = Raylib.LoadTexture("Sprites/0x72/0x72_DungeonTilesetII_v1.4/frames/chest_full_open_anim_f0.png");
        pos = new Vector2(Rect.x,Rect.y);
    }
    public override void Draw()
    {
        Raylib.DrawRectangleRec(Rect, Color.DARKBLUE);
        Raylib.DrawTextureEx(texture,pos,0,scale,Color.WHITE);
    }
}
