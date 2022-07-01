using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using NeonnCore.actor;
using NeonnCore.messaging;
using Raylib_cs;

namespace NeonnGraphics;

public class GraphicsManager : Monolith, IActor
{
    public static GraphicsManager Instance;
    private static bool IsInit = false;
    public static Regex Uri = MonolithUri.Graphics;
    private static Dictionary<String, Texture2D> TextureCache = new Dictionary<string, Texture2D>();
    private static Dictionary<String, SpriteSheet> SpriteSheetCache = new Dictionary<string, SpriteSheet>();
    
    public GraphicsManager()
    {
        if (!IsInit)
        {
            Instance = this;
            IsInit = true;
        }
    }
    
    public static void Subscribe(EngineDatagram datagram)
    {
        var m = Uri.Match(datagram.Recipient);
        if (m.Success)
        {
            switch (datagram.Operation)
            {
                case Operation.Create:
                    Instance.Create(datagram);
                    break;
                case Operation.Read:
                    Instance.Read(datagram);
                    break;
                case Operation.Update:
                    Instance.Update(datagram);
                    break;
                case Operation.Delete:
                    Instance.Delete(datagram);
                    break;
            }  
        }
      
    }
    
    public void SendMessage()
    {
        
    }
    //TODO: Uses SendMessage to send a datagram
    public void Create(EngineDatagram datagram)
    {
        
    }
    //TODO: Reads a datagram from the Monolith static method 'public static void Subscribe(EngineDatagram datagram)'
    public void Read(EngineDatagram datagram)
    {
        
    }
    //TODO: Uses SendMessage to send a datagram
    public void Update(EngineDatagram datagram)
    {
        
    }
    //TODO: Uses SendMessage to send a datagram
    public void Delete(EngineDatagram datagram)
    {
        
    }
}