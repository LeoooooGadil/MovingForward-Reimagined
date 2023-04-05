using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextIndicator : MonoBehaviour
{

    public Text levelText;
    private int currentLevel = 1;

    // implement indicating the level of the player
    // make sure to update the text when the level changes
    // make sure to update the text when the experience changes
    void Update()
    {
        if (ExperienceManager.instance != null)
        {
            int level = ExperienceManager.instance.GetExperienceSave().GetLevel();
            if (level != currentLevel)
            {
                currentLevel = level;
                levelText.text = currentLevel.ToString();
            }
        }
    }
}
