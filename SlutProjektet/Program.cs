﻿global using Raylib_cs;
global using System.Numerics;
global using System.IO;
global using System.Text.Json;

Raylib.InitWindow(1920, 1080, "Världens sämsta spel");
Raylib.SetTargetFPS(60);

//Draws Background
Map map = new Map();
//List of Entity, contains all entities

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

    //GRAFIK
    //Draw Map, Player and Enemies
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.WHITE);
    map.Draw();
    foreach (Item i in Controller.items)
    {
        i.Draw();
    }
    foreach (Entity e in Controller.entities)
    {
        e.Draw();
    }
    Raylib.EndDrawing();
}