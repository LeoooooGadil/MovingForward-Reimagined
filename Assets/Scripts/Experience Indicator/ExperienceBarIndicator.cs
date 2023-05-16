using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBarIndicator : MonoBehaviour
{
    public Image ExperienceBar;
    public Text ExperienceText;

    private float experienceNormalized = 0;
    private float currentExperience = 0;

    private Coroutine updateCoroutine;

    void Start()
    {
        // Start the coroutine to update the experience text and bar
        updateCoroutine = StartCoroutine(UpdateExperience());
    }

    IEnumerator UpdateExperience()
    {
        while (true)
        {
            try
            {
                ExperienceSave experienceSave = ExperienceManager.instance.GetExperienceSave();

                float maxExperienceNeededToLevelUp = experienceSave.GetExperienceToNextLevel();
                float newExperience = experienceSave.GetExperience();

                if (newExperience != currentExperience)
                {
                    currentExperience = newExperience;

                    StartCoroutine(UpdateExperienceText());
                    StartCoroutine(UpdateExperienceBar(maxExperienceNeededToLevelUp));
                }
            }
            catch (System.Exception)
            {
                // Handle exception
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator UpdateExperienceText()
    {
        float targetExperience = currentExperience;
        float startingExperience = float.Parse(ExperienceText.text);

        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime * 5;
            float currentValue = Mathf.Lerp(startingExperience, targetExperience, timer);
            ExperienceText.text = Mathf.Round(currentValue).ToString("F0");
            yield return null;
        }
    }

    IEnumerator UpdateExperienceBar(float maxExperienceNeededToLevelUp)
    {
        float targetNormalizedValue = ConvertExperienceToNormalized(0f, maxExperienceNeededToLevelUp, currentExperience);
        float startingNormalizedValue = experienceNormalized;

        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime * 5;
            experienceNormalized = Mathf.Lerp(startingNormalizedValue, targetNormalizedValue, timer);
            ExperienceBar.fillAmount = experienceNormalized;
            yield return null;
        }
    }

    float ConvertExperienceToNormalized(float min, float max, float experience)
    {
        float fromMin = min;
        float fromMax = max;
        float toMin = 0f;
        float toMax = 1f;

        float experienceNormalized = (experience - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;

        return Mathf.Round(experienceNormalized * 100) / 100;
    }
}
