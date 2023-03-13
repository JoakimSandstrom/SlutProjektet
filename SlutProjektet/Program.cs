global using Raylib_cs;
global using System.Numerics;
global using System.IO;
global using System.Text.Json;

Raylib.InitWindow(960, 960, "Världens sämsta spel");
Raylib.SetTargetFPS(60);

//Draws Background
Map map = new Map();
//List of Entity, contains all entities
List<Entity> entities = new();
//Keeps track of time, difficulty and spawns enemies
Controller controller = new Controller(entities);


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