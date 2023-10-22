[System.Serializable]
public class SaveData
{
    public int highscore;
    public int kills;
    public int deaths;
    public int bosskills;
    public int powerups;

    public SaveData(int highscoreInt, int killsInt, int deathsInt, int bosskillsInt, int powerupsInt)
    {
        highscore = highscoreInt;
        kills = killsInt;
        deaths = deathsInt;
        bosskills = bosskillsInt;
        powerups = powerupsInt;
    }
}