
public class Slime : Enemy
{
    public Slime(float x, float y, List<Entity> entities) : base(entities)
    {
        //Set Variables
        name = "Slime";
        Speed = 2f;
        Str = 1;
        Health = 3;
        frameSize = 48;
        border = true;
        columnWidth = 12;
        spriteSheet = "Sprites/dungeon-pack-free_version/sprite/free_monsters_0.png";
        animationFile = "SlimeAnimations.json";

        NewEnemy(x, y);
    }
}
