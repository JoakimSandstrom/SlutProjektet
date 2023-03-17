public class Heart : Item
{
    public Heart(List<Entity> entities)
    {
        Rarity = "common";
        PickUp(entities);
    }
    public override void PickUp(List<Entity> entities)
    {
        entities[0].Health += 1;
    }
}
