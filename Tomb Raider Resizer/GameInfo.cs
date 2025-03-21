using System.Collections.Generic;

namespace Tomb_Raider_Resizer
{
    /// <summary>
    /// Represents a game and its associated process names.
    /// </summary>
    public class GameInfo
    {
        public string Title { get; set; }
        public List<string> ProcessNames { get; set; }

        public GameInfo(string title, params string[] processNames)
        {
            Title = title;
            ProcessNames = new List<string>(processNames);
        }

        public override string ToString() => Title;
    }
}
