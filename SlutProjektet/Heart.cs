public class Heart : Item
{
    private float hpIncrease = 25;
    private float hpAddition;
    static Heart()
    {
        texture = Raylib.LoadTexture("Sprites/0x72/0x72_DungeonTilesetII_v1.4/frames/ui_heart_full.png");
        Rarity = "common";
        id = 0;
    }
    public override void PickUp()
    {
        amount += 1;
        hpAddition += amount*hpIncrease;
    }
}
