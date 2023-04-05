public class Chest : Interactive
{
    static Chest()
    {
        objectTextures.Add("Chest",Raylib.LoadTexture("Sprites/0x72/0x72_DungeonTilesetII_v1.4/frames/chest_full_open_anim_f0.png"));
    }
    public Chest()
    {
        Rect = new Rectangle(400,400,16*scale,16*scale);
        pos = new Vector2(Rect.x,Rect.y);
        texture = objectTextures["Chest"];
    }
}
