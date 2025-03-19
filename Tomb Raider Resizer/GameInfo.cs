using System.Collections.Generic;

namespace TombRaiderResizer
{
    /// <summary>
    /// Represents a game and its associated process names.
    /// </summary>
    public class GameInfo
    {
        public string Title { get; set; }
        public List<string> ProcessNames { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameInfo"/> class with the specified title and process names.
        /// </summary>
        /// <param name="title">The title of the game.</param>
        /// <param name="processNames">One or more process names associated with the game.</param>
        public GameInfo(string title, params string[] processNames)
        {
            Title = title;
            ProcessNames = new List<string>(processNames);
        }

        /// <summary>
        /// Returns the title of the game.
        /// </summary>
        public override string ToString() => Title;
    }
}
