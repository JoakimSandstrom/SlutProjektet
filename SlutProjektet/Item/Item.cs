public class Item
{
    //static variables
    protected static int scale = Map.scale;

    //List/Dictionary of texture file paths and item textures
    public static Dictionary<int,Texture2D> itemTextures = new();
    public static List<string> itemTexturePaths = new();
    
    //Non-static variables
    public string Rarity {get; protected set;} = "";
    protected Texture2D texture;
    protected int id;
    protected int amount = 0;

    //Do something on pickup (increase stat...)
    public virtual void PickUp(Entity e)
    {
        
    }
}
