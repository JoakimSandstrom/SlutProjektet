global using Raylib_cs;
global using System.Numerics;

Raylib.InitWindow(960, 960, "Världens sämsta spel");
Raylib.SetTargetFPS(60);

Map map = new Map();
Controller controller = new Controller();
Player p = new Player();

//Creates a list of enemies and adds one to start
List<Enemy> enemies = new List<Enemy>();
enemies.Add(new Enemy(200,200));

while(!Raylib.WindowShouldClose())
{
    //LOGIK
    //Uppdate Controller, Player and Enemies
    controller.GameTime(enemies);
    p.Update(enemies);
    foreach (Enemy e in enemies)
    {
        e.Update(p);
    }

    //GRAFIK
    //Draw Map, Player and Enemies
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    map.Draw();
    p.Draw();
    foreach (Enemy e in enemies)
    {
        e.Draw();
    }
    Raylib.EndDrawing();
}