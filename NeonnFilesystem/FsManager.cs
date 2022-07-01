using System.Runtime.InteropServices;
using System.Text;
using Zio;
using Zio.FileSystems;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Raylib_cs;

namespace NeonnFilesystem;

public class FsManager
{
    private ZipArchiveFileSystem Res;
    private string DataPath;
    private string AppName;
    private string DataProtocol;

    private Regex Uri;
    private Platform CurrentPlatform;


    public FsManager(String resPath, String appName)
    {
        Uri = new Regex(@"^(?<proto>\w+):/(?<path>.+)", RegexOptions.None);
        AppName = appName;
        DataProtocol = "data:/";
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            CurrentPlatform = Platform.Linux;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            CurrentPlatform = Platform.Windows;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            CurrentPlatform = Platform.MacOS;
        }
        
        var bundleChecker = new PhysicalFileSystem();
        if (bundleChecker.FileExists(resPath))
        {
            Res = new ZipArchiveFileSystem(resPath);
        }
        
        if (CurrentPlatform == Platform.Linux)
        {
            string user = Environment.UserName;
            DataPath = "/home/" + user + "/.local/share/neonn/" + AppName;
        }
        else if (CurrentPlatform == Platform.MacOS)
        {
            string user = Environment.UserName;
            DataPath = "/Users/" + user + "/Library/Application Support/neonn/" + AppName;
        }
        
        if (DataPath != null && !bundleChecker.DirectoryExists(DataPath))
        {
            bundleChecker.CreateDirectory(DataPath);
        }
        
    }

    public Image LoadImage(String filepath)
    {
        Match m = Uri.Match(filepath);
        if (m.Success && m.Result("${path}").EndsWith(".png"))
        {
            UPath path = new UPath(m.Result("${path}"));
            Stream fs = Res.OpenFile(path, FileMode.Open, FileAccess.Read, FileShare.None);
            Byte[] data;
            using (var ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                data = ms.ToArray();
            }
            
            string ext = ".png";
            byte[] bytes = Encoding.ASCII.GetBytes(ext);
            
            unsafe
            {
                byte* imgdata;
                fixed (Byte* d = data)
                {
                    imgdata = (byte*)d;
                }
                fixed (byte* p = bytes)
                {
                    sbyte* extension = (sbyte*)p;  
                    //SP is now what you want
                    Image img = Raylib.LoadImageFromMemory(extension, imgdata, data.Length);
                    return img;

                }               
            }
            
        }
        else
        {
            Image img = new Image();
            return img;
        }
    }
    
    public void WriteJson(String filePath, object data)
    {
        Match m = Uri.Match(filePath);
        if (m.Success && m.Result("${proto}") == "data")
        {
            //Console.WriteLine(m.Result("${path}"));
            var fs = new PhysicalFileSystem();

            UPath path = new UPath(DataPath + m.Result("${path}"));
            
            Stream stream = fs.OpenFile(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            
            using (StreamWriter writer = new StreamWriter(stream))
            using (JsonTextWriter jsonWriter = new JsonTextWriter(writer))
            {
                JsonSerializer ser = new JsonSerializer();
                ser.Serialize(jsonWriter, data);
                jsonWriter.Flush();
            }

        }
    }
    
    public T ReadJson<T>(String file, FsType underlyingFsType)
    {
        Match m = Uri.Match(file);
        if (m.Success)
        {
            if (underlyingFsType == FsType.Physical)
            {
                if (m.Result("${proto}") == "data")
                {
                    var fs = new PhysicalFileSystem();
                    UPath path = new UPath(DataPath + m.Result("${path}"));
                    if (fs.FileExists(path))
                    {
                        Stream stream = fs.OpenFile(path, FileMode.Open, FileAccess.Read);
                        using (StreamReader reader = new StreamReader(stream))
                        using (JsonTextReader jsonReader = new JsonTextReader(reader))
                        {
                            JsonSerializer ser = new JsonSerializer();
                            return ser.Deserialize<T>(jsonReader);
                        } 
                    }
                    else
                    {
                        return default(T);
                    } 
                }

                return default(T);
            }
            
            else if (underlyingFsType == FsType.Zip)
            {
                if (m.Result("${proto}") == "res")
                {
                    var fs = Res;
                    UPath path = new UPath(DataPath + m.Result("${path}"));
                    if (fs.FileExists(path))
                    {
                        Stream stream = fs.OpenFile(path, FileMode.Open, FileAccess.Read);
                        using (StreamReader reader = new StreamReader(stream))
                        using (JsonTextReader jsonReader = new JsonTextReader(reader))
                        {
                            JsonSerializer ser = new JsonSerializer();
                            return ser.Deserialize<T>(jsonReader);
                        } 
                    }
                    else
                    {
                        return default(T);
                    }
                }
                return default(T);
            }
        }
        return default(T);
    }

    public AppConf ReadConf()
    {
        AppConf conf = ReadJson<AppConf>("res://conf.json", FsType.Zip);
        return conf;
    }

}