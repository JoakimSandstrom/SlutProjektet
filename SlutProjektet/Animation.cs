public class Animation
{
    private string spriteSheetName;

    //Changing Variables
    public List<int> frame;
    private int frameSize;
    private int columnWidth;
    private int borderSize = 0;

    //Set Variables
    private Rectangle source;
    private int frameIndex = 0;
    private int row = 0;

    //Let's other classes set next animations
    public Animation next;

    //Timer variables
    private float timerMaxValue;
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
        this.frameSize = frameSize;
        this.columnWidth = columnWidth;
        this.frame = new List<int>(frame);

        //if sprite has a border => remove it.
        if (border) this.borderSize = frameSize / 3;

        //Timer variables
        this.timerMaxValue = timerMaxValue;
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
            timerCurrentValue = timerMaxValue;

            if (frameIndex == frame.Count - 1) frameIndex = 0;
            else frameIndex++;
        }

        //Sets row in spritsheet
        if(frame[frameIndex] != 0) row = frame[frameIndex] / columnWidth;
        else row = 0;

        //Sets rectangle at correct location in spritesheet
        source.x = ((frame[frameIndex] % columnWidth) * frameSize) + borderSize;
        source.y = (row * frameSize) + borderSize;
        source.width = frameSize - (borderSize * 2);
        source.height = frameSize - (borderSize * 2);

        //Draw frame
        Raylib.DrawTexturePro(spriteSheets[spriteSheetName], source, e.animRect, Vector2.Zero, 0, Color.WHITE);
    }
}
