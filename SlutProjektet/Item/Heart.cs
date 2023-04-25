public class Heart : Item
{
    //Variables
    private float hpIncrease = 25;
    private float hpAddition;

    static Heart()
    {
        //Load texture once
        if (!itemTextures.ContainsKey(0)) itemTextures.Add(0,Raylib.LoadTexture(itemTexturePaths[0]));
    }
    public Heart()
    {
        //Variables
        id = 0;
        Rarity = "common";
        texture = itemTextures[id];
    }
    public override void PickUp(Entity e)
    {
        amount += 1;
        //Increase HP
        hpAddition += amount*hpIncrease;
        
    }
}
