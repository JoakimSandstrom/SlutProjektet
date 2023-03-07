public class Player : Entety
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
    private int[] aDownAttack = {3,4,5};
    private int[] aLeftAttack = {15,16,17};
    private int[] aRightAttack = {27,28,29};
    private int[] aUpAttack = {39,40,41};

    //Timer / attack Cool Down
    private float attackCD;
    public bool IsAttacking {get;private set;} = false;

    //Collision variables
    private bool colliding = false;
    private Vector2 collPoint1;
    private Vector2 collPoint2;
    private Vector2 collPoint3;

    public Player()
    {
        //Set Player stats
        name = "player";
        Speed = 5f;
        Str = 1;
        frameSize = 32;

        //Set player rectangles to keep track of position, collistion and attacking
        animRect = new Rectangle(480, 480, 32*scale, 32*scale);
        attackBox = new Rectangle(animRect.x+24, animRect.y+24, 20*scale, 20*scale);
        hitBox = new Rectangle(animRect.x+30,animRect.y+12,12*scale,16*scale);

        //Load player Animations
        animations.Add("aDownStop", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aDownStop, 12, animSpeed, false));
        animations.Add("aDown", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aDown, 12, animSpeed, false));
        animations.Add("aLeftStop", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aLeftStop, 12, animSpeed, false));
        animations.Add("aLeft", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aLeft, 12, animSpeed, false));
        animations.Add("aRightStop", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aRightStop, 12, animSpeed, false));
        animations.Add("aRight", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aRight, 12, animSpeed, false));
        animations.Add("aUpStop", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aUpStop, 12, animSpeed, false));
        animations.Add("aUp", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aUp, 12, animSpeed, false));
        animations.Add("aDownAttack", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aDownAttack, 12, animSpeed/2, false));
        animations.Add("aLeftAttack", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aLeftAttack, 12, animSpeed/2, false));
        animations.Add("aRightAttack", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aRightAttack, 12, animSpeed/2, false));
        animations.Add("aUpAttack", new Animation("Sprites/dungeon-pack-free_version/sprite/free_character_0.png", frameSize, aUpAttack, 12, animSpeed/2, false));

        //Set Next Animation
        animations["aDown"].next = animations["aDownStop"];
        animations["aLeft"].next = animations["aLeftStop"];
        animations["aRight"].next = animations["aRightStop"];
        animations["aUp"].next = animations["aUpStop"];
        animations["aDownAttack"].next = animations["aDownStop"];
        animations["aLeftAttack"].next = animations["aLeftStop"];
        animations["aRightAttack"].next = animations["aRightStop"];
        animations["aUpAttack"].next = animations["aUpStop"];

        //Set Starting Animation
        currentAnimation = animations["aDownStop"];
    }

    //Every frame
    public void Update(List<Enemy> enemies)
    {
        //Keep track of InvFrames
        if (InvFrame > 0) InvFrame -= Raylib.GetFrameTime();

        //Update timer
        if (attackCD > 0)
        {
            attackCD -= Raylib.GetFrameTime();
            IsAttacking = true;
            return;
        }
        IsAttacking = false;

        //Reset Vector2
        movement = Vector2.Zero;

    //**Controlls**
        //Attack controlls and method
        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {
            //Change current animation to an attack animation
            if (animIndex.Contains("Stop")) animIndex = animIndex.Substring(0,animIndex.LastIndexOf("Stop"));
            if (!animIndex.Contains("Attack")) animIndex += "Attack";
            currentAnimation = animations[animIndex];
            attackCD = animSpeed*1.5f;
            Attack(enemies);
            //Return to stop moving when attacking
            return;
        }

        //Movement controlls
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S) || Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) {movement.Y = 1; animIndex = "aDown"; isMoving = true;}
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A) || Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) {movement.X = -1; animIndex = "aLeft"; isMoving = true;}
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D) || Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) {movement.X = 1; animIndex = "aRight"; isMoving = true;}
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W) || Raylib.IsKeyDown(KeyboardKey.KEY_UP)) {movement.Y = -1; animIndex = "aUp"; isMoving = true;}

        //Normalize Vector2 if not 0 which would break the code.
        if (movement.Length() > 0)
        {
            movement = Vector2.Normalize(movement) * Speed;
        }

        //Add Vector2 to Player position
        //If new position is outside the playable area, push the Player back in
        animRect.x += movement.X;
        hitBox.x += movement.X;
        CheckCollision("x");
        animRect.y += movement.Y;
        hitBox.y += movement.Y;
        CheckCollision("y");

        //Change Animation State
        if (isMoving) currentAnimation = animations[animIndex];
        else if (!isMoving) currentAnimation = currentAnimation.next;
        isMoving = false;
    }

    //Move attack hitbox and deal damage
    public void Attack(List<Enemy> enemies)
    {
        switch (animIndex)
        {
            case "aDownAttack":
                attackBox.x = animRect.x + 18;
                attackBox.y = animRect.y + 48;
                attackBox.height = 16*scale;
                attackBox.width = 23*scale;
                break;
            case "aLeftAttack":
                attackBox.x = animRect.x;
                attackBox.y = animRect.y + 18;
                attackBox.height = 23*scale;
                attackBox.width = 16*scale;
                break;
            case "aRightAttack":
                attackBox.x = animRect.x + 48;
                attackBox.y = animRect.y + 18;
                attackBox.height = 23*scale;
                attackBox.width = 16*scale;
                break;
            case "aUpAttack":
                attackBox.x = animRect.x + 9;
                attackBox.y = animRect.y;
                attackBox.height = 16*scale;
                attackBox.width = 23*scale;
                break;
        }

        //Check if enemy is in range, if true do GetHit()
        foreach (Enemy e in enemies)
        {
            if (Raylib.CheckCollisionRecs(attackBox,e.hitBox))
            {
                e.GetHit(Str);
            }
        }
    }

    //Checks and enforces collision
    private void CheckCollision(string s)
    {
        //Set points
        collPoint1.X = hitBox.x;
        collPoint1.Y = hitBox.y+hitBox.height;
        collPoint2.X = hitBox.x+hitBox.width;
        collPoint2.Y = hitBox.y+hitBox.height;
        collPoint3.X = hitBox.x+(hitBox.width/2);
        collPoint3.Y = hitBox.y+(hitBox.height*0.75f);

        //Check collision for every collision object
        foreach (Rectangle r in Map.collision)
        {
            //if colliding, move back
            if (Raylib.CheckCollisionPointRec(collPoint1, r) || Raylib.CheckCollisionPointRec(collPoint2, r) || Raylib.CheckCollisionPointRec(collPoint3, r))
            {
                if (s == "x") 
                {
                    animRect.x -= movement.X;
                    hitBox.x -= movement.X;
                }
                else if (s == "y")
                {
                    animRect.y -= movement.Y;
                    hitBox.y -= movement.Y;
                }
            }
        }
    }

    //Draw to screen
    public void Draw()
    {
        //Raylib.DrawRectangleRec(hitBox, Color.DARKBLUE);
        currentAnimation.Draw(this);
    }
}
