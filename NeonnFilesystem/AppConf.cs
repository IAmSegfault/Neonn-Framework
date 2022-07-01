using Newtonsoft.Json;
namespace NeonnFilesystem;

public class AppConf
{
    //App name
    public String appid;
    //Semantic version
    public String version;
    //Should we use external storage on android?
    public String externalstorage;
    //Set the framework backend
    public String backend;
    //The window title
    public string windowtitle;
    //Path to a window icon for desktop version
    public string windowicon;
    //Default screen width
    public int windowwidth;
    //Default screen height
    public int windowheight;
    //Should the screen run in borderless mode?
    public bool windowborderless;
    //Allow resizing the window
    public bool windowresizable;
    //Should we start in fullscreen mode?
    public bool windowfullscreen;
    //If so do we want to run in fullscreen exclusive mode?
    public string windowfullscreenmode;
    //Should vsync be enabled?
    public bool windowvsync;
    //Our target framerate. Set it to something high like 10000 if we want unlimited.
    public int windowtargetfps;
    
    public AppConf()
    {
        
    }
}