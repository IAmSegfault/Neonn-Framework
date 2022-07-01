using Raylib_cs;
namespace NeonnCore.windowing;

public class Window
{
    public static bool IsInit = false;
    public Window(int width, int height, string title)
    {
        if (!Window.IsInit)
        {
            Raylib.InitWindow(width, height, title);
            Window.IsInit = true;
        }
        
    }

    public bool RequestQuit()
    {
        return Raylib.WindowShouldClose();
    }
}