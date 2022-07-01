using Raylib_cs;
namespace NeonnCore.core;

public class Time
{
    public static readonly long SECOND = 1000000000;
    private static float delta;

    public static double GetTime()
    {
        return Raylib.GetTime();
    }

    public static void setDelta(float delta)
    {
        Time.delta = delta;
    }
}