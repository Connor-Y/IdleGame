using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AchievementTracker : Singleton<AchievementTracker> {

    public int x = 0;

    private Dictionary<int, Achievement> achievementDict;
    private StatTracker stats;

    private int numOfAchievements;

	void Awake () {
        stats = StatTracker.Instance;
        achievementDict = new Dictionary<int, Achievement>();

        Achievement a1 = new Achievement("earned 10", earned10);
        achievementDict.Add(1, a1);

        numOfAchievements = achievementDict.Count;
    }


    void Update()
    {
        checkAll();
    }

    public int getNumOfAchievements()
    {
        return numOfAchievements;
    }

    private AchievementTracker() { }

    private bool earned10()
    {
        if (stats.getMoney() > stats.getStartingMoney() + 10)
            return true;
        return false;
    }

    private bool checkOne(Achievement a)
    {
        return a.checkCompletion();
    }

    public void checkAll()
    {
        for (int i = 1; i <= numOfAchievements; i++)
        {
            if (!checkValidId(i))
            {
                return;
            }
            if (checkOne(achievementDict[i]))
            {
                // TODO: Handle Achievement here
            }
        }
    }

    public Achievement getAchievementWithId(int id)
    {
        return achievementDict[id];
    }

    private bool checkValidId(int id)
    {
        //Debug.Log("ID " + id);
        if (achievementDict.ContainsKey(id))
            return true;

        return false;
    }

}
