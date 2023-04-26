//Work in progress
//Not currently being used

public class TitleScreen : UI
{
    private static Texture2D background = Raylib.LoadTexture("Sprites/Background.png");
    
    public static void Draw()
    {
        Raylib.DrawTextureEx(background, Vector2.Zero, 0, 1, Color.WHITE);
    }
}
