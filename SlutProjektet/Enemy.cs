public class Enemy : Entity
{
    //Player index in list of entities
    protected int playerIndex;

    //Animations
    /*
    private int[] aDownStop = {1};
    private int[] aDown = {0,1,2,1};
    private int[] aLeftStop = {13};
    private int[] aLeft = {12,13,14,13};
    private int[] aRightStop = {25};
    private int[] aRight = {24,25,26,25};
    private int[] aUpStop = {37};
    private int[] aUp = {36,37,38,37};
    */

    protected float distance = 0;
    protected float timer = 0.48f;

    public Enemy(List<Entity> entities)
    {
        /*Set variables
        name
        Speed
        Str
        Health
        frameSize
        border
        spriteSheet
        animationFile
        animSpeed
        */

        baseInvFrame = 0.5f;
        InvFrame = baseInvFrame;
        
        //Set refrence to Player
        //Player must be instantiated before enemy or this wont work
        foreach (Entity e in entities)
        {
            if (e is Player)
            {
                playerIndex = entities.IndexOf(e);
            }
        }
    }
    
    //This controlls the AI
    public override void Update(List<Entity> entities)
    {
        //If enemy is dead, don't continue
        if (Dead) return;
        
        //Keep track of InvFrames
        if (InvFrame > 0) InvFrame -= Raylib.GetFrameTime();

        //Hit Player
        if (Raylib.CheckCollisionRecs(hitBox,entities[playerIndex].hitBox))
        {
            entities[playerIndex].GetHit(Str);
        }

        //Calculate direction to move
        if (distance <= -48f)
        {
            //Reset Vector2
            movement = Vector2.Zero;

            //Get the relative position of the player
            movement.X = (entities[playerIndex].animRect.x + 24) - animRect.x;
            movement.Y = (entities[playerIndex].animRect.y + 24) - animRect.y;

            //If on player then don't proceed
            if ((((entities[playerIndex].animRect.x + 24) - animRect.x) <= 6 && ((entities[playerIndex].animRect.x + 24) - animRect.x) >= -6 ) && ((entities[playerIndex].animRect.y + 24) - animRect.y) <= 6 && ((entities[playerIndex].animRect.y + 24) - animRect.y) >= -6) return;

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
    public override void Draw()
    {
        //Don't draw if dead
        if (Dead) return;
        //Raylib.DrawRectangleRec(hitBox, Color.DARKGREEN);
        currentAnimation.Draw(this);
    }
    
    //Sets rectangles and fetcches animations
    public virtual void NewEnemy(float x, float y)
    {
        //Set Enemy rectangle to keep track of position and collision
        animRect = new Rectangle(x, y, frameSize, frameSize);
        hitBox = new Rectangle(animRect.x,animRect.y,frameSize,frameSize);

        //Load Animations
        AnimationDeserializer(animationFile,spriteSheet,frameSize,columnWidth,animSpeed,border);

        //Set Next Animation
        animations["aDown"].next = animations["aDownStop"];
        animations["aLeft"].next = animations["aLeftStop"];
        animations["aRight"].next = animations["aRightStop"];
        animations["aUp"].next = animations["aUpStop"];

        //Set Starting Animation
        currentAnimation = animations["aDownStop"];
    }
}

