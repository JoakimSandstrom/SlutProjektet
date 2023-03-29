public class Entity
{
    public string name = "bob";

    //Raylib variables
    protected Vector2 movement = new();
    protected int scale = Map.scale;

    protected Dictionary<int,Item> items = new Dictionary<int,Item>();


    //Hitbox
    public Rectangle attackBox;
    public Rectangle animRect;
    public Rectangle hitBox;
    public Rectangle collisionBox;

    //Stats
    public float Speed {get; set;}
    public float BaseSpeed {get; protected set;}
    public int Health {get; set;}
    public int BaseHealth {get; protected set;}
    public int Str {get; set;}
    public int BaseStr {get; protected set;}
    public float InvFrame {get; set;} = 1f;
    protected float baseInvFrame = 1f;
    public bool Dead {get; set;} = false;

    //Animation dictionary and variables
    protected Dictionary<string, Animation> animations = new();
    protected Animation currentAnimation;
    protected string animIndex = "aDownStop";
    protected string animationFile;
    protected string spriteSheet;
    protected int frameSize;
    protected float animSpeed = 0.12f;
    protected bool border;
    protected int columnWidth;

    protected bool isMoving = false;

    //Get hit if no invinsibility frames
    public void GetHit(int damage)
    {
        if (InvFrame <= 0)
        {
            Health -= damage;
            InvFrame = baseInvFrame;
            Console.WriteLine(name+Health);
        }
        if (Health <= 0) Death();
    }

    public virtual void Update()
    {

    }
    public virtual void Draw()
    {

    }
    public void AnimationSerializer()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        Dictionary<string,List<int>> serializedAnimations = new();
        foreach (var v in animations)
        {
            serializedAnimations.Add(v.Key,animations[v.Key].frame);
        }
        string json = JsonSerializer.Serialize<Dictionary<string, List<int>>>(serializedAnimations, options);
        File.WriteAllText(animationFile, json);
    }
    public virtual void AnimationDeserializer(string animationFile, string spriteSheet, int frameSize,int columnWidth, float animSpeed, bool border)
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
            animations.Add(v.Key, new Animation(spriteSheet, frameSize, v.Value, columnWidth, animSpeed, border));
        }
    }
    public void AddItem(ItemPickup i)
    {
        if (!items.ContainsKey(i.itemId)) items.Add(i.itemId,i.itemMaker[i.itemId]());
        items[i.itemId].PickUp();
    }
    public virtual void Death()
    {

    }
}

