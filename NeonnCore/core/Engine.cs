namespace NeonnCore.core;
using NeonnCore.windowing;

public class Engine
{
    private bool IsRunning;
    private int Width;
    private int Height;
    private double FrameTime;
    private Window Window;
    
    public Engine(int width, int height, double framerate)
    {
        IsRunning = false;
        Width = width;
        Height = height;
        FrameTime = 1.0d / framerate;
        
    }

    public void CreateWindow(String title)
    {
        Window = new Window(Width, Height, title);
    }
    
    public void Start(String windowTitle)
    {
        if (IsRunning)
        {
            return;
        }
        CreateWindow(windowTitle);
        Run();
    }

    public void Stop()
    {
        if (!IsRunning)
        {
            return;
        }

        IsRunning = false;
    }

    public void Run()
    {
        IsRunning = true;
        int frames = 0;
        double frameCounter = 0.0d;
        double lastTime = Time.GetTime();
        double unprocessedTime = 0.0d;

        while (IsRunning)
        {
            bool render = false;
            double startTime = Time.GetTime();
            double elapsedTime = startTime - lastTime;
            unprocessedTime += elapsedTime;
            frameCounter += elapsedTime;

            while (unprocessedTime > FrameTime)
            {
                render = true;
                unprocessedTime -= FrameTime;
                if (Window.RequestQuit())
                {
                    Stop();
                }
                Input.update();
                Update();
                
            }

            if (render)
            {
                Render();
                frames++;
            }
        }
    }
    
    public void Update()
    {
        
    }

    public void Render()
    {
        
    }

    public void CleanUp()
    {
        
    }
}