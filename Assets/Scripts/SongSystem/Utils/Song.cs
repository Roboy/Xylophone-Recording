using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XylophoneHero.SongSystem.Utils
{
    public class Song
    {
        public string Name;
        public List<string> Content;

        public Song()
        {
            Name = "Untitled";
            Content = null;
        }

        public Song(string name, List<string> songData)
        {
            Name = name;
            Content = songData;
        }
    }
}
