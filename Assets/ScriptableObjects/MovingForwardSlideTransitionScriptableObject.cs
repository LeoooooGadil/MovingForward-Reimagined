using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Slide Transition", menuName = "Moving Forward/Scene Transitons/Slide")]
public class MovingForwardSlideTransitionScriptableObject : MovingForwardAbstractSceneTransitionScriptableObject
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public Direction direction = Direction.Left;
    public Color Color;

	public override IEnumerator Enter(Canvas Parent)
	{
        float time = 0;
        Vector2 initialPosition = AnimatedObject.rectTransform.anchoredPosition;
        Vector2 finalPosition = Vector2.zero;
        switch (direction)
        {
            case Direction.Left:
                finalPosition = new Vector2(-Screen.width, 0);
                break;
            case Direction.Right:
                finalPosition = new Vector2(Screen.width, 0);
                break;
            case Direction.Up:
                finalPosition = new Vector2(0, Screen.height);
                break;
            case Direction.Down:
                finalPosition = new Vector2(0, -Screen.height);
                break;
        }

        while (time < 1)
        {
            AnimatedObject.rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, finalPosition, time);
            time += Time.deltaTime / AnimationTime;
            yield return null;
        }

        Destroy(AnimatedObject.gameObject);
	}

	public override IEnumerator Exit(Canvas Parent)
	{
        AnimatedObject = CreateImage(Parent);
        AnimatedObject.color = Color;
        AnimatedObject.rectTransform.anchorMin = Vector2.zero;
        AnimatedObject.rectTransform.anchorMax = Vector2.one;
        AnimatedObject.rectTransform.sizeDelta = Vector2.zero;

        float time = 0;
        Vector2 initialPosition = Vector2.zero;
        Vector2 finalPosition = Vector2.zero;
        switch (direction)
        {
            case Direction.Left:
                initialPosition = new Vector2(Screen.width, 0);
                break;
            case Direction.Right:
                initialPosition = new Vector2(-Screen.width, 0);
                break;
            case Direction.Up:
                initialPosition = new Vector2(0, -Screen.height);
                break;
            case Direction.Down:
                initialPosition = new Vector2(0, Screen.height);
                break;
        }

        while (time < 1)
        {
            AnimatedObject.rectTransform.anchoredPosition = Vector2.Lerp(initialPosition, finalPosition, time);
            time += Time.deltaTime / AnimationTime;
            yield return null;
        }
	}
}
