using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Circle Transition", menuName = "Moving Forward/Scene Transitons/Circle")]
public class MovingForwardCircleTransitionScriptableObject : MovingForwardAbstractSceneTransitionScriptableObject
{
	public Sprite CircleSprite;
	public Color Color;

	public override IEnumerator Enter(Canvas Parent)
	{
		float time = 0;
		float size = Mathf.Sqrt(
			Mathf.Pow(Screen.width, 2) + Mathf.Pow(Screen.height, 2)
		);
		Vector2 initialSize = new Vector2(size, size);
		while (time < 1)
		{
            if(AnimatedObject != null)
                AnimatedObject.rectTransform.sizeDelta = Vector2.Lerp(initialSize, Vector2.zero, time);
            yield return null;
            time += Time.deltaTime / AnimationTime;
		}

        Destroy(AnimatedObject.gameObject);
	}

	public override IEnumerator Exit(Canvas Parent)
	{
		AnimatedObject = CreateImage(Parent);
        AnimatedObject.color = Color;
        AnimatedObject.rectTransform.sizeDelta = Vector2.zero;
        AnimatedObject.sprite = CircleSprite;

        float time = 0;
        float size = Mathf.Sqrt(
            Mathf.Pow(Screen.width, 2) + Mathf.Pow(Screen.height, 2)
        );
        Vector2 finalSize = new Vector2(size, size);
        while (time < 1)
        {
            if(AnimatedObject != null)
                AnimatedObject.rectTransform.sizeDelta = Vector2.Lerp(Vector2.zero, finalSize, time);
            time += Time.deltaTime / AnimationTime;
            yield return null;
        }
	}
}
