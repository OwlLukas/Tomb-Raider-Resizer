using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tomb_Raider_Resizer
{
    public class GameInfo
    {
        public string Title { get; set; }
        public List<string> ProcessNames { get; set; }

        // Konstruktor mit variabler Anzahl von Prozessnamen
        public GameInfo(string title, params string[] processNames)
        {
            Title = title;
            ProcessNames = new List<string>(processNames);
        }

        // Überschreiben von ToString, falls du die Objekte z.B. in einer ListBox anzeigen möchtest
        public override string ToString()
        {
            return Title;
        }
    }

}
