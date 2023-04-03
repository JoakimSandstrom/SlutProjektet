global using Raylib_cs;
global using System.Numerics;
global using System.IO;
global using System.Text.Json;

Raylib.InitWindow(940, 940, "Världens sämsta spel");
Raylib.SetTargetFPS(60);

//Draws Background
Map map = new Map();

//Keeps track of time, difficulty and spawns enemies
Controller controller = new Controller();


while(!Raylib.WindowShouldClose())
{
    //LOGIK
    //Uppdate Controller, Player and Enemies
    controller.GameTime();
    foreach (Entity e in Controller.entities)
    {
        e.Update();
    }
    HUD.Update((Player)Controller.entities[Controller.playerIndex]);

    //GRAFIK
    //Draw Map, Player and Enemies
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    map.Draw();
    foreach (ItemPickup i in Controller.itemPickups)
    {
        i.Draw();
    }
    foreach (Entity e in Controller.entities)
    {
        e.Draw();
    }
    HUD.Draw();
    Raylib.EndDrawing();
}