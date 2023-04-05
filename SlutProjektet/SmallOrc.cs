public class SmallOrc : Enemy
{
    public SmallOrc(float x, float y)
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

    //This enemy doesn't have up and down animations
    protected override void NewEnemy(float x, float y)
    {
        //Set Enemy rectangle to keep track of position and collision
        animRect = new Rectangle(x, y, frameSize*3, frameSize*3);
        hitBox = new Rectangle(animRect.x,animRect.y,frameSize*3,frameSize*3);

        //Load Animations
        AnimationDeserializer(animationFile,spriteSheet,frameSize,columnWidth,animSpeed,border);

        //Set Next Animation
        animations["aRight"].next = animations["aRightStop"];
        animations["aLeft"].next = animations["aLeftStop"];

        //Set Starting Animation
        currentAnimation = animations["aRightStop"];
    }
    
    //Slight change to acount for the fact that it only has animations for moving left and right
    protected override void Direction()
    {
        //Calculate direction to move
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
            else if (movement.Y >= 0 && Math.Abs(movement.Y) >= Math.Abs(movement.X)) {movement.X = 0; movement.Y = Speed; animIndex = "aRight";}
            else if (movement.Y <= 0 && Math.Abs(movement.Y) >= Math.Abs(movement.X)) {movement.X = 0; movement.Y = -Speed; animIndex = "aLeft";}
            else Console.WriteLine("Error!!!");

            //Change Animation State
            currentAnimation = animations[animIndex];

            //Reset distance
            distance = 48f;
            timer = 0.48f;
        }
    }
}
