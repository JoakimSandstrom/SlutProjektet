global using Raylib_cs;
global using System.Numerics;
global using System.IO;
global using System.Text.Json;

Raylib.InitWindow(960, 960, "Världens sämsta spel");
Raylib.SetTargetFPS(60);

Map map = new Map();
Controller controller = new Controller();
Player p = new Player();

//Creates a list of entities and adds one to start
List<Entity> entities = new();
entities.Add(new Enemy(200,200,entities));
entities.Add(new Player());

while(!Raylib.WindowShouldClose())
{
    //LOGIK
    //Uppdate Controller, Player and Enemies
    controller.GameTime(entities);
    foreach (Entity e in entities)
    {
        e.Update(entities);
    }

    //GRAFIK
    //Draw Map, Player and Enemies
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    map.Draw();
    foreach (Entity e in entities)
    {
        e.Draw();
    }
    Raylib.EndDrawing();
}