using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fade Transition", menuName = "Moving Forward/Scene Transitons/Fade")]
public class MovingForwardFadeTransitionScriptableObject : MovingForwardAbstractSceneTransitionScriptableObject
{
	public Color maxColor = Color.black;

	public override IEnumerator Enter(Canvas Parent)
	{
		float time = 0;
		Color startColor = maxColor;
		Color endColor = new Color(maxColor.r, maxColor.g, maxColor.b, 0);

		while (time < 1)
		{
            if(AnimatedObject != null)
                AnimatedObject.color = Color.Lerp(startColor, endColor, time);
            yield return null;
            time += Time.deltaTime / AnimationTime;
		}

        if(AnimatedObject != null)
            Destroy(AnimatedObject.gameObject);
	}

	public override IEnumerator Exit(Canvas Parent)
	{
		AnimatedObject = CreateImage(Parent);
        AnimatedObject.rectTransform.anchorMin = Vector2.zero;
        AnimatedObject.rectTransform.anchorMax = Vector2.one;
        AnimatedObject.rectTransform.sizeDelta = Vector2.zero;

        float time = 0;
        Color startColor = new Color(maxColor.r, maxColor.g, maxColor.b, 0);
        Color endColor = maxColor;

        while (time < 1)
        {
            if(AnimatedObject != null)
                AnimatedObject.color = Color.Lerp(startColor, endColor, time);
            yield return null;
            time += Time.deltaTime / AnimationTime;
        }
	}
}
