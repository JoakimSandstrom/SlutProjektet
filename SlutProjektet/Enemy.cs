public class Enemy: Entety
{
    //Animations
    private int[] aDownStop = {1};
    private int[] aDown = {0,1,2,1};
    private int[] aLeftStop = {13};
    private int[] aLeft = {12,13,14,13};
    private int[] aRightStop = {25};
    private int[] aRight = {24,25,26,25};
    private int[] aUpStop = {37};
    private int[] aUp = {36,37,38,37};

    private float distance = 0;
    private float timer = 0.48f;

    public Enemy(float x, float y)
    {
        //Set Stats
        name = "Enemy";
        Speed = 2f;
        Str = 1;
        Health = 3;
        frameSize = 48;

        //Set Enemy rectangle to keep track of position and collision
        animRect = new Rectangle(x, y, 48, 48);
        hitBox = new Rectangle(animRect.x,animRect.y,48,48);

        //Load player Animations
        animations.Add("aDownStop", new Animation("Sprites/dungeon-pack-free_version/sprite/free_monsters_0.png", frameSize, aDownStop, 12, animSpeed, true));
        animations.Add("aDown", new Animation("Sprites/dungeon-pack-free_version/sprite/free_monsters_0.png", frameSize, aDown, 12, animSpeed, true));
        animations.Add("aLeftStop", new Animation("Sprites/dungeon-pack-free_version/sprite/free_monsters_0.png", frameSize, aLeftStop, 12, animSpeed, true));
        animations.Add("aLeft", new Animation("Sprites/dungeon-pack-free_version/sprite/free_monsters_0.png", frameSize, aLeft, 12, animSpeed, true));
        animations.Add("aRightStop", new Animation("Sprites/dungeon-pack-free_version/sprite/free_monsters_0.png", frameSize, aRightStop, 12, animSpeed, true));
        animations.Add("aRight", new Animation("Sprites/dungeon-pack-free_version/sprite/free_monsters_0.png", frameSize, aRight, 12, animSpeed, true));
        animations.Add("aUpStop", new Animation("Sprites/dungeon-pack-free_version/sprite/free_monsters_0.png", frameSize, aUpStop, 12, animSpeed, true));
        animations.Add("aUp", new Animation("Sprites/dungeon-pack-free_version/sprite/free_monsters_0.png", frameSize, aUp, 12, animSpeed, true));

        //Set Next Animation
        animations["aDown"].next = animations["aDownStop"];
        animations["aLeft"].next = animations["aLeftStop"];
        animations["aRight"].next = animations["aRightStop"];
        animations["aUp"].next = animations["aUpStop"];

        //Set Starting Animation
        currentAnimation = animations["aDownStop"];
    }
    
    //This controlls the AI
    public void Update(Player p)
    {
        //If enemy is dead, don't continue
        if (Dead) return;
        
        //Keep track of InvFrames
        if (InvFrame > 0) InvFrame -= Raylib.GetFrameTime();

        //Hit Player
        if (Raylib.CheckCollisionRecs(hitBox,p.hitBox))
        {
            p.GetHit(Str);
        }

        //Calculate direction to move
        if (distance <= -48f)
        {
            //Reset Vector2
            movement = Vector2.Zero;

            //Get the relative position of the player
            movement.X = (p.animRect.x + 24) - animRect.x;
            movement.Y = (p.animRect.y + 24) - animRect.y;

            //If on player then don't proceed
            if ((((p.animRect.x + 24) - animRect.x) <= 6 && ((p.animRect.x + 24) - animRect.x) >= -6 ) && ((p.animRect.y + 24) - animRect.y) <= 6 && ((p.animRect.y + 24) - animRect.y) >= -6) return;

            //Check directions and set speed and animation
            if (movement.X >= 0 && Math.Abs(movement.X) >= Math.Abs(movement.Y)) {movement.X = Speed; movement.Y = 0; animIndex = "aRight";}
            else if (movement.X <= 0 && Math.Abs(movement.X) >= Math.Abs(movement.Y)) {movement.X = -Speed; movement.Y = 0; animIndex = "aLeft";}
            else if (movement.Y >= 0 && Math.Abs(movement.Y) >= Math.Abs(movement.X)) {movement.X = 0; movement.Y = Speed; animIndex = "aDown";}
            else if (movement.Y <= 0 && Math.Abs(movement.Y) >= Math.Abs(movement.X)) {movement.X = 0; movement.Y = -Speed; animIndex = "aUp";}
            else Console.WriteLine("Error!!!");

            //Change Animation State
            currentAnimation = animations[animIndex];

            //Reset distance
            distance = 48f;
            timer = 0.48f;
        }

        //Decrease distance and timer
        distance -= Speed;
        timer -= Raylib.GetFrameTime();
        
        //Move
        if (timer > 0)
        {
            //Add Vector2 to Enemy position
            animRect.x += movement.X;
            animRect.y += movement.Y;
            hitBox.x += movement.X;
            hitBox.y += movement.Y;
        }

        //Stop Animation
        if (timer < 0 && !animIndex.Contains("Stop"))
        {
            currentAnimation = currentAnimation.next;
        }
    }

    //Draw to screen
    public void Draw()
    {
        //Don't draw if dead
        if (Dead) return;
        //Raylib.DrawRectangleRec(hitBox, Color.DARKGREEN);
        currentAnimation.Draw(this);
    }
}

