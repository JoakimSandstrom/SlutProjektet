public class ItemPickup : Object
{
    //Item id, same as the item class
    public int itemId {get; private set;}
    
    static ItemPickup()
    {
        //Deserialize list of itemTexturePaths once
        string jsonText = File.ReadAllText("Json/ItemTextures.json");
        Item.itemTexturePaths = JsonSerializer.Deserialize<List<string>>(jsonText);
    }
    public ItemPickup(Vector2 pos,int itemId)
    {
        //Variables
        Rect = new Rectangle(pos.X,pos.Y,16*scale,16*scale);
        this.pos = pos;
        this.itemId = itemId;

        //Add texture
        if (!Item.itemTextures.ContainsKey(itemId)) Item.itemTextures.Add(itemId,Raylib.LoadTexture(Item.itemTexturePaths[itemId]));
        texture = Item.itemTextures[itemId];
    }

    //Return instance of different class based on index
    public Func<Item>[] itemMaker = 
    {
        () => new Heart()
    };

}
