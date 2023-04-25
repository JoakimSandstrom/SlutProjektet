public class Enemy : Entity
{
    //Variables
    protected float distance = 0; //Distance to player
    protected float timer = 0.48f; //Interval for movement

    protected float itemChance = 1f; //Chanche to drop item

    public Enemy()
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

        BaseInvFrame = 0.5f;
        InvFrame = BaseInvFrame;
    }
    
    //This controlls the AI
    public override void Update()
    {
        //If enemy is dead, don't continue
        if (Dead) return;
        
        //Keep track of InvFrames
        if (InvFrame > 0) InvFrame -= Raylib.GetFrameTime();

        //Hit Player
        if (Raylib.CheckCollisionRecs(hitBox,Controller.entities[Controller.playerIndex].hitBox))
        {
            Controller.entities[Controller.playerIndex].GetHit(Str);
        }

        Direction();

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

    //Calculate direction to move
    protected virtual void Direction()
    {
        if (distance <= -48f)
        {
            //Reset Vector2
            movement = Vector2.Zero;

            //Get the relative position of the player
            movement.X = (Controller.entities[Controller.playerIndex].animRect.x + 24) - animRect.x;
            movement.Y = (Controller.entities[Controller.playerIndex].animRect.y + 24) - animRect.y;

            //If on player then don't proceed
            if ((((Controller.entities[Controller.playerIndex].animRect.x + 24) - animRect.x) <= 6 && ((Controller.entities[Controller.playerIndex].animRect.x + 24) - animRect.x) >= -6 ) && ((Controller.entities[Controller.playerIndex].animRect.y + 24) - animRect.y) <= 6 && ((Controller.entities[Controller.playerIndex].animRect.y + 24) - animRect.y) >= -6) return;

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
    protected virtual void NewEnemy(float x, float y)
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

    //Drops item and sets "Dead" to true
    protected override void Death()
    {
        Vector2 pos = new();
        pos.X = hitBox.x;
        pos.Y = hitBox.y;
        Dead = true;
        if (itemChance >= (float)(Controller.random.NextDouble()))
        {
            Controller.SpawnItem(pos,0);
        }
    }
}

