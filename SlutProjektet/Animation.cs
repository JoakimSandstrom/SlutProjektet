public class Animation
{
    private string spriteSheetName;

    //Json serialized variables
    public string SpriteSheetFile {get; set;}
    public int FrameSize { get; set;}
    public List<int> Frame {get; set;}
    public int ColumnWidth {get; set;}
    public float TimerMaxValue {get; set;}
    public bool Border {get; set;}

    //Changing Variables
    //private List<int> frame;
    //private int frameSize;
    //private int columnWidth;
    private int borderSize = 0;

    //Set Variables
    private Rectangle source;
    private int frameIndex = 0;
    private int row = 0;

    //Let's other classes set next animations
    public Animation next;

    //Timer variables
    //private float timerMaxValue;
    private float timerCurrentValue;

    //Dictionary of loaded textures
    private static Dictionary<string, Texture2D> spriteSheets = new();

    
    public Animation(string spriteSheetFile, int frameSize, int[] frame, int columnWidth, float timerMaxValue, bool border)
    {
        //Load new textures
        if (!spriteSheets.ContainsKey(spriteSheetFile))
        {
            spriteSheets.Add(spriteSheetFile, Raylib.LoadTexture(spriteSheetFile));
        }
        spriteSheetName = spriteSheetFile;

        //Set variables
        this.FrameSize = frameSize;
        this.ColumnWidth = columnWidth;
        this.Frame = new List<int>(frame);

        //if sprite has a border => remove it.
        if (border) this.borderSize = frameSize / 3;

        //Timer variables
        this.TimerMaxValue = timerMaxValue;
        timerCurrentValue = timerMaxValue;

        next = this;
    }

    //Draw the current frame of the animation
    public void Draw(Entity e)
    {
        timerCurrentValue -= Raylib.GetFrameTime();
        //when timer hits 0, reset and go to next frame of animation
        if (timerCurrentValue < 0)
        {
            timerCurrentValue = TimerMaxValue;

            if (frameIndex == Frame.Count - 1) frameIndex = 0;
            else frameIndex++;
        }

        //Sets row in spritsheet
        if(Frame[frameIndex] != 0) row = Frame[frameIndex] / ColumnWidth;
        else row = 0;

        //Sets rectangle at correct location in spritesheet
        source.x = ((Frame[frameIndex] % ColumnWidth) * FrameSize) + borderSize;
        source.y = (row * FrameSize) + borderSize;
        source.width = FrameSize - (borderSize * 2);
        source.height = FrameSize - (borderSize * 2);

        //Draw frame
        Raylib.DrawTexturePro(spriteSheets[spriteSheetName], source, e.animRect, Vector2.Zero, 0, Color.WHITE);
    }
}
