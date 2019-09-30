using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSystem_Persistency
{
[Serializable()]
public class CD
{
    [field: NonSerialized()] private int playingTime;

    public string Title { get; set; }
    public int PlayingTime { get { return playingTime; } set { playingTime = value; } }
    public int YearReleased { get; set; }
    public string Artist { get; set; }

    public CD(string title, int playingTime, int yearReleased, string artist)
    {
        this.Title = title;
        this.PlayingTime = playingTime;
        this.YearReleased = yearReleased;
        this.Artist = artist;
    }

    public override string ToString()
    {
        return Title + " by " + Artist + ". Released in " + YearReleased + ". Playing time: " + PlayingTime;
    }
}
}
