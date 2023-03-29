public class Player : Entity
{
    //Timer / attack Cool Down
    private float attackCD;
    private bool isAttacking;
    private float baseAttackCD;

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
        collisionBox = new Rectangle(hitBox.x,hitBox.y+(hitBox.height*0.75f),hitBox.width,hitBox.height*0.25f);

        AnimationDeserializer(animationFile,spriteSheet,frameSize,columnWidth,animSpeed,border);

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
    public override void Update()
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
        if (Input() && !isAttacking) Attack();

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
        collisionBox.x += movement.X;
        CheckCollision("x");
        animRect.y += movement.Y;
        hitBox.y += movement.Y;
        attackBox.y += movement.Y;
        collisionBox.y += movement.Y;
        CheckCollision("y");

        //Change Animation State
        if (!isAttacking) currentAnimation = animations[animIndex];
        isMoving = false;
    }

    //Move attack hitbox and deal damage
    public void Attack()
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
        foreach (Entity e in Controller.entities)
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
        //Check collision for every collision object
        foreach (Rectangle r in Map.collision)
        {
            //if colliding, move back
            if (Raylib.CheckCollisionRecs(collisionBox,r))
            {
                if (s == "x") 
                {
                    animRect.x -= movement.X;
                    hitBox.x -= movement.X;
                    attackBox.x -= movement.X;
                    collisionBox.x -= movement.X;
                }
                else if (s == "y")
                {
                    animRect.y -= movement.Y;
                    hitBox.y -= movement.Y;
                    attackBox.y -= movement.Y;
                    collisionBox.y -= movement.Y;
                }
            }
        }
        foreach (ItemPickup i in Controller.itemPickups)
        {
            if (Raylib.CheckCollisionRecs(collisionBox,i.Rect))
            {
                AddItem(i);
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
            else animations.Add(v.Key, new Animation(spriteSheet, frameSize, v.Value, 12, baseAttackCD/3, false));
        }
    }
}
