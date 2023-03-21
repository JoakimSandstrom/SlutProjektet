public class Heart : Item
{
    static Heart()
    {
        texture = Raylib.LoadTexture("Sprites/0x72/0x72_DungeonTilesetII_v1.4/frames/ui_heart_full.png");
        Rarity = "common";
    }

    public Heart(Vector2 pos)
    {
        rect = new Rectangle(0,0,16*scale,16*scale);
        rect.x = pos.X;
        rect.y = pos.Y;
    }
    public override void PickUp()
    {
        Controller.entities[0].Health += 1;
    }
}
