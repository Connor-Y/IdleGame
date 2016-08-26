using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Instantiation : MonoBehaviour
{

    public GameObject buttonPrefab;
    public GameObject buttonParent;
    public UpgradeController upgradeController;

    public GameObject achievementImageDefaultPrefab;
    public GameObject achievementParent;
    public AchievementTracker achievementTracker;


    public float gridX;
    public float gridY;
    public float edgeOffsetX;
    public float edgeOffsetY;
    public float borderX;
    public float borderY;
    public float spacingX = 0f;
    public float spacingY = 0f;

    private int numOfUpgrades;
    private int numOfAchievements;

    void Start()
    {
        upgradeInstatiator();
        achievementInstatiator();
    }

    private void upgradeInstatiator()
    {
        RectTransform t = buttonPrefab.GetComponent<RectTransform>();
        spacingX = t.rect.width + borderX;
        spacingY = -1 * (t.rect.height + borderY);
        numOfUpgrades = upgradeController.getNumOfUpgrades();
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                int id = ((int)(y * gridX + x + 1));
                // Do not create more buttons than available upgrades
                if (id > numOfUpgrades)
                    break;

                // Instatiate new button at location in grid
                Vector3 pos = new Vector3(x * spacingX + edgeOffsetX, y * spacingY + edgeOffsetY, 0);
                GameObject obj = (GameObject) Instantiate(buttonPrefab, pos, Quaternion.identity);
                // Set the parent object
                obj.transform.SetParent(buttonParent.transform, false);
                // Set button text
                string buttonText = "Upgrade " + id;
                obj.GetComponentInChildren<Text>().text = buttonText;

                // Add onClick listener to purchase upgrade
                Button button = obj.GetComponent<Button>();
                button.onClick.AddListener(() => { upgradeController.purchaseBuildingUpgrade(id); });
            
                // Add the Entity handler (to control button graphics)
                EntityHandler entityHandler = obj.AddComponent<EntityHandler>();
                // Set the UpgradeObjects id and enity handler to correct id/handler
                UpgradeObject upObj = upgradeController.getUpgradeWithId(id);
                upObj.setId(id);
                upObj.setEntityHandler(entityHandler);

            }
        }
    }

    private void achievementInstatiator()
    {
        RectTransform t = achievementImageDefaultPrefab.GetComponent<RectTransform>();
        spacingX = t.rect.width + borderX;
        spacingY = -1 * (t.rect.height + borderY);
        numOfAchievements = achievementTracker.getNumOfAchievements();
        for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                int id = ((int)(y * gridX + x + 1));
                // Do not create more buttons than available upgrades
                if (id > 5)
                    break;
                Debug.Log("ID: " + id);
                Debug.Log("x " + x + " y " + y);
                // Instatiate new button at location in grid
                Vector3 pos = new Vector3(x * spacingX + edgeOffsetX, y * spacingY + edgeOffsetY, 0);
                GameObject obj = (GameObject)Instantiate(achievementImageDefaultPrefab, pos, Quaternion.identity);
                // Set the parent object
                obj.transform.SetParent(achievementParent.transform, false);


                // Add the Entity handler (to control button graphics)
               // EntityHandler entityHandler = obj.AddComponent<EntityHandler>();
                // Set the UpgradeObjects id and enity handler to correct id/handler
               // Achievement a = achievementTracker.getAchievementWithId(id);
               // a.setEntityHandler(entityHandler);

            }
        }
    }

}

