public class SmallOrc : Enemy
{
    public SmallOrc(float x, float y, List<Entity> entities) : base(entities)
    {
        //Set Variables
        name = "SmallOrc";
        Speed = 3f;
        Str = 2;
        Health = 3;
        frameSize = 16;
        border = false;
        columnWidth = 64;
        animSpeed = 0.06f;
        spriteSheet = "Sprites/0x72/dungeontiles-extended v1.1/dungeontileset-extended1.png";
        animationFile = "SmallOrcAnimations.json";
        distance = 48f;
        timer = 0.48f;

        NewEnemy(x, y);
    }

    public override void NewEnemy(float x, float y)
    {
        //Set Enemy rectangle to keep track of position and collision
        animRect = new Rectangle(x, y, frameSize*3, frameSize*3);
        hitBox = new Rectangle(animRect.x,animRect.y,frameSize*3,frameSize*3);

        //Load Animations
        AnimationDeserializer(animationFile,spriteSheet,frameSize,columnWidth,animSpeed,border);

        //Set Next Animation
        //animations["aLeft"].next = animations["aLeftStop"];
        animations["aRight"].next = animations["aRightStop"];
        animations["aLeft"].next = animations["aLeftStop"];

        //Set Starting Animation
        currentAnimation = animations["aRightStop"];
    }
    
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
            if ((((entities[playerIndex].animRect.x + 24) - animRect.x) <= 6 && ((entities[playerIndex].animRect.x + 24) - animRect.x) >= -6 ) && ((entities[playerIndex].animRect.y + 24) - animRect.y) <= 6 && ((entities[playerIndex].animRect.y + 24) - animRect.y) >= -6)
            {
                //Change to idle animation
                if (!animIndex.Contains("Stop"))
                {
                    currentAnimation = currentAnimation.next;
                }
                return;
            }
            //Check directions and set speed and animation
            if (movement.X >= 0 && Math.Abs(movement.X) >= Math.Abs(movement.Y)) {movement.X = Speed; movement.Y = 0; animIndex = "aRight";}
            else if (movement.X <= 0 && Math.Abs(movement.X) >= Math.Abs(movement.Y)) {movement.X = -Speed; movement.Y = 0; animIndex = "aLeft";}
            else if (movement.Y >= 0 && Math.Abs(movement.Y) >= Math.Abs(movement.X)) {movement.X = 0; movement.Y = Speed; animIndex = "aRight";}
            else if (movement.Y <= 0 && Math.Abs(movement.Y) >= Math.Abs(movement.X)) {movement.X = 0; movement.Y = -Speed; animIndex = "aLeft";}
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
    }
}
