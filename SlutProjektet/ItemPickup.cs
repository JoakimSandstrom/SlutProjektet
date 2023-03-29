public class ItemPickup : Object
{
    public int itemId {get; private set;}
    public ItemPickup(Vector2 pos,int itemId)
    {

        Rect = new Rectangle(pos.X,pos.Y,16*scale,16*scale);
        this.pos = pos;

        this.itemId = itemId;

    }

    public Func<Item>[] itemMaker = 
    {
        () => new Heart()
    };

}
