public class ItemPickup : Object
{
    public int itemId {get; private set;}
    public static List<string> itemTexturePaths = new();
    static ItemPickup()
    {
        string jsonText = File.ReadAllText("ItemTextures.json");
        itemTexturePaths = JsonSerializer.Deserialize<List<string>>(jsonText);
    }
    public ItemPickup(Vector2 pos,int itemId)
    {

        Rect = new Rectangle(pos.X,pos.Y,16*scale,16*scale);
        this.pos = pos;

        this.itemId = itemId;

        if (!itemTextures.ContainsKey(itemId)) itemTextures.Add(itemId,Raylib.LoadTexture(itemTexturePaths[itemId]));
        texture = itemTextures[itemId];
    }

    public Func<Item>[] itemMaker = 
    {
        () => new Heart()
    };

}
