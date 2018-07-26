using System;
using System.Collections.Generic;

namespace XylophoneHero.SongSystem.Utils
{
    [Serializable]
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
