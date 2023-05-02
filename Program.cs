using System.Numerics;
using System;
using Raylib_cs;
using static Raylib_cs.Raylib;
class Planet{
    static readonly float G = 1f, Multiplier = -1f;
    public static float GMUL = 1f;
    public static List<Planet> planets = new();
    public float Mass = 0f;
    public bool ignoreGravity = false;
    public Color clr = Color.RAYWHITE;
    public Vector2 Velocity = Vector2.Zero;
    public Vector2 Position = Vector2.Zero, force;
    public void ApplyForce(){
        Vector2 accel = force / Mass;
        Velocity += accel * Multiplier;// * GetFrameTime();
    }
    public static void CalculateGravitys(){
        for (int i = 0; i < planets.Count; i++){
            planets[i].force = Vector2.Zero;
            if (planets[i].ignoreGravity)
                continue;
            foreach (Planet p in planets){
                if (p == planets[i])
                    continue;
                Vector2 direction = planets[i].Position - p.Position;
                float distance = direction.Length();
                float forceMagnitude = (G * GMUL) * planets[i].Mass * p.Mass / (distance * distance);
                planets[i].force += Vector2.Normalize(direction) * forceMagnitude;
            }
            planets[i].ApplyForce();
            planets[i].Position += planets[i].Velocity;
        }
    }

}
class Program{
    static Vector2 targetpos = new(0,0);
    static int target = -1, fps = 120, track = -1;
    static bool autoclear = false;
    public static Dictionary<string, Color> clrs = new Dictionary<string, Color>();
    public static void Main(string[] args){
        InitColors();
        string filename = "config";
        if(args.Length > 0){
            if(File.Exists(args[0])){
                filename = args[0];
            }
        }
        string[] config = File.ReadAllLines(filename);
        //Load everything from config file
        foreach(string line in config){
            //Checking if special
            if(line == String.Empty || line[0] == '$')
                continue;
            if(line.ToUpper() == "CLEAR"){
                autoclear = true;
                continue;
            }else if(line.Split(' ')[0].ToUpper() == "FOCUS"){
                target = int.Parse(line.Split(' ')[1]);
                continue;
            }else if(line.Split(' ')[0].ToUpper() == "FRAMERATE"){
                fps = int.Parse(line.Split(' ')[1]);
                continue;
            }else if(line.Split(' ')[0].ToUpper() == "DVEL"){
                track = int.Parse(line.Split(' ')[1]);
                continue;
            }else if(line.Split(' ')[0].ToUpper() == "CONSTANT"){
                Planet.GMUL = int.Parse(line.Split(' ')[1]);
                continue;
            }
            //Parsing the line itself
            string[] parts = line.Replace(" ","").Split(',');
            Vector2 Position = new(ParseNumber(parts[0]),ParseNumber(parts[1]));
            float Mass = ParseNumber(parts[2]);
            Vector2 Velocity = new(ParseNumber(parts[3]),ParseNumber(parts[4]));
            Color clr = clrs[parts[5]];
            bool staticPlanet = (parts.Length >= 7 && parts[6] == "TRUE");
            Planet.planets.Add(new Planet(){
                Position = Position,
                Mass = Mass,
                Velocity = Velocity,
                clr = clr,
                ignoreGravity = staticPlanet});
        }
        //Intial setup
        SetTraceLogLevel(TraceLogLevel.LOG_NONE);
        InitWindow(700,700,"Simulator");
        SetExitKey(KeyboardKey.KEY_ESCAPE);
        SetTargetFPS(fps);
        //Main loop
        while(!WindowShouldClose()){
            //Autoclear
            if(autoclear)
                ClearBackground(Color.BLACK);
            //Targeting camera
            if(target != -1){
                targetpos = Planet.planets[target].Position;
                targetpos.X-= 350;
                targetpos.Y-= 350;
            }
            //Registering keypressess
            if(IsKeyPressed(KeyboardKey.KEY_F11)){
                ToggleFullscreen();
            }else if(IsKeyPressed(KeyboardKey.KEY_C)){
                ClearBackground(Color.BLACK);
            }else{
                if(IsKeyDown(KeyboardKey.KEY_SPACE)){
                    Planet.planets[^1].Velocity *= 1.001f;
                }else if(IsKeyDown(KeyboardKey.KEY_LEFT_SHIFT)){
                    Planet.planets[^1].Velocity *= 0.999f;
                }
            }
            BeginDrawing();
            BeginMode2D(new Camera2D(Vector2.Zero,targetpos,0f,1f));
            //Draws positions for debuging
            if(track > -1){
                DrawRectangle(0,0,100,15,Color.BLACK);
                DrawText(Planet.planets[track].Velocity.Length().ToString(),0,0,15,Color.YELLOW);
            }
            
            Planet.CalculateGravitys();
            //Deawing every planet
            foreach(Planet planet in Planet.planets){
                DrawCircle((int)planet.Position.X, (int)planet.Position.Y, (int)(planet.Mass /2) + 1, planet.clr);
            }
            EndMode2D();
            EndDrawing();
        }
    }
    static float ParseNumber(string inr){
        return float.Parse(inr.Replace(".",","),System.Globalization.NumberStyles.Any);
    }
    static void InitColors(){
        clrs.Add("BLUE", Color.BLUE);
        clrs.Add("RED", Color.RED);
        clrs.Add("GREEN", Color.GREEN);
        clrs.Add("YELLOW", Color.YELLOW);
        clrs.Add("GRAY", Color.GRAY);
        clrs.Add("ORANGE", Color.ORANGE);
    }
}