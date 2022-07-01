using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace NeonnGraphics;

public class SpriteSheet
{
    private Raylib_cs.Image Image;
    private List<Rectangle> Frames;
    private Vector2 ImageDimensions;
    private Vector2 FrameDimensions;
    private int NumFrames;
    public String TextureUri;
    public String Uri;
    
    public unsafe SpriteSheet(String uri, String imgType, byte[] imgData, int imgSize, Vector2 imageDimensions, Vector2 frameDimensions)
    {
        Uri = uri;
        ImageDimensions = imageDimensions;
        FrameDimensions = frameDimensions;
        int cols = (int)imageDimensions.X / (int)frameDimensions.X;
        int rows = (int) imageDimensions.Y / (int) frameDimensions.Y;
        NumFrames = cols * rows;

        IntPtr imgTypePointer = IntPtr.Zero;
        
        fixed(byte* imgDataPtr = imgData)
        {
            try
            {
                imgTypePointer = Marshal.StringToHGlobalAnsi(imgType);
                Image = Raylib_cs.Raylib.LoadImageFromMemory((sbyte*) imgTypePointer, imgDataPtr, imgSize);
            }
            finally
            {
                Marshal.FreeHGlobal(imgTypePointer);
            }
        }
    }

    public Image BindTexture(String textureUri)
    {
        TextureUri = textureUri;
        return Image;
    }
}