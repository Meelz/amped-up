using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class Game_Data
{
    //public static string songDirectory = "C:/Users/Emil/Documents/amped up/Songs";
    //for internal testing
    //public static string songDirectory = "C:/Users/Emil/Documents/amped up/week 11 build/Songs";

    //for game build
    public static string songDirectory = "./Songs";

    public static bool validSongDir = true;
    public static Song_Parser.Metadata chosenSongData;
    public static Song_Parser.difficulties difficulty = Song_Parser.difficulties.hard;
    public static float currentScore = 0.0f;
    public static int maxCombo = 0;
    public static int notesHit = 0;
    public static int noteCount = 0;
    public static bool hasPassed = true;
}

