public class Player : Entity
{
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
    private int[] aDownAttack = {3,4,5};
    private int[] aLeftAttack = {15,16,17};
    private int[] aRightAttack = {27,28,29};
    private int[] aUpAttack = {39,40,41};
    */


    //Timer / attack Cool Down
    private float attackCD;
    private bool isAttacking;
    private float baseAttackCD;
    

    //Collision variables
    private bool colliding = false;
    private Vector2 collPoint1;
    private Vector2 collPoint2;
    private Vector2 collPoint3;

    public Player()
    {
        //Set Player stats
        name = "player";
        BaseSpeed = 5f;
        Speed = BaseSpeed;
        Str = 1;
        frameSize = 32;
        border = false;
        columnWidth = 12;
        baseAttackCD = animSpeed*3f;
        animationFile = "PlayerAnimations.json";
        spriteSheet = "Sprites/dungeon-pack-free_version/sprite/free_character_0.png";
        baseInvFrame = 1f;

        //Set player rectangles to keep track of position, collistion and attacking
        animRect = new Rectangle(480, 480, 32*scale, 32*scale);
        attackBox = new Rectangle(animRect.x+24, animRect.y+24, 20*scale, 20*scale);
        hitBox = new Rectangle(animRect.x+30,animRect.y+12,12*scale,16*scale);

        AnimationDeserializer(animationFile,spriteSheet,frameSize,columnWidth,animSpeed,border);

        //Load player Animations
        /*
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
        */

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

        //AnimationSerializer();
    }

    //Every frame
    public override void Update(List<Entity> entities)
    {
        //Keep track of InvFrames
        if (InvFrame > 0) InvFrame -= Raylib.GetFrameTime();

        //Update timer
        if (attackCD > 0)
        {
            attackCD -= Raylib.GetFrameTime();
            isAttacking = true;
        }
        else isAttacking = false;

        //Controlls
        if (Input() && !isAttacking) Attack(entities);

        if (isMoving) Movement();
        else if (!isAttacking) currentAnimation = currentAnimation.next;
    }

    public bool Input()
    {
        //Reset Vector2
        movement = Vector2.Zero;
        //Movement controlls
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) {movement.Y = 1; animIndex = "aDown"; isMoving = true;}
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) {movement.X = -1; animIndex = "aLeft"; isMoving = true;}
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) {movement.X = 1; animIndex = "aRight"; isMoving = true;}
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) {movement.Y = -1; animIndex = "aUp"; isMoving = true;}
        //Attack controlls
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)) {animIndex = "aDownAttack"; return true;}
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) {animIndex = "aLeftAttack"; return true;}
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) {animIndex = "aRightAttack"; return true;}
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP)) {animIndex = "aUpAttack"; return true;}
        return false;
    }

    //Move Player
    public void Movement()
    {
        //Lower Speed if attacking
        Speed = BaseSpeed;
        if (isAttacking) 
        {
            Speed = Speed / 2;
        }

        //Normalize Vector2 if not 0 which would break the code.
        if (movement.Length() > 0)
        {
            movement = Vector2.Normalize(movement) * Speed;
        }

        //Add Vector2 to Player position
        //If new position is outside the playable area, push the Player back in
        animRect.x += movement.X;
        hitBox.x += movement.X;
        attackBox.x += movement.X;
        CheckCollision("x");
        animRect.y += movement.Y;
        hitBox.y += movement.Y;
        attackBox.y += movement.Y;
        CheckCollision("y");

        //Change Animation State
        if (!isAttacking) currentAnimation = animations[animIndex];
        isMoving = false;
    }

    //Move attack hitbox and deal damage
    public void Attack(List<Entity> entities)
    {
        currentAnimation = animations[animIndex];
        isAttacking = true;
        attackCD = baseAttackCD;

        //Move attackBox based on attacking directions
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
        foreach (Entity e in entities)
        {
            if (e is Player) continue;
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
                    attackBox.x -= movement.X;
                }
                else if (s == "y")
                {
                    animRect.y -= movement.Y;
                    hitBox.y -= movement.Y;
                    attackBox.y -= movement.Y;
                }
            }
        }
    }

    //Draw to screen
    public override void Draw()
    {
        Raylib.DrawRectangleRec(attackBox, Color.DARKBLUE);
        currentAnimation.Draw(this);
    }

    //Json
    public override void AnimationDeserializer(string animationFile, string spriteSheet, int frameSize,int columnWidth, float animSpeed, bool border)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        string jsonText = File.ReadAllText(animationFile);
        //Deserialize dictionary containing animations
        Dictionary<string, int[]> deserializedAnimation = JsonSerializer.Deserialize<Dictionary<string, int[]>>(jsonText, options);
        //Add animations using deseralizedAnimations
        foreach(var v in deserializedAnimation)
        {
            //Attack animations are slower
            if(!v.Key.Contains("Attack"))
            {
                animations.Add(v.Key, new Animation(spriteSheet, frameSize, v.Value, 12, animSpeed, false));
            }
            else animations.Add(v.Key, new Animation(spriteSheet, frameSize, v.Value, 12, attackCD/3, false));
        }
    }
}
