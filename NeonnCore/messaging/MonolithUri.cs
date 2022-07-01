using System.Text.RegularExpressions;

namespace NeonnCore.messaging;

public class MonolithUri
{
    public static Regex Graphics = new Regex(@"^monolith://graphics.(?<path>.+)", RegexOptions.None);
}