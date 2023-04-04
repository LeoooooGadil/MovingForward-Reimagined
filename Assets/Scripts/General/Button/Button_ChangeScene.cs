using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_ChangeScene : MonoBehaviour
{
	public string sceneName;
    public DialogAnimator dialogAnimator;

    private Button button;

	void Start()
	{
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeScene);
	}

    void ChangeScene()
    {
        if (dialogAnimator != null)
        {
            StartCoroutine(WaitAndChangeScene());
            return;
        }

        AudioManager.instance.PlaySFX("PopClick");
        LevelManager.instance.ChangeScene(sceneName);
    }

    IEnumerator WaitAndChangeScene()
    {
        dialogAnimator.ExitDialog();
        AudioManager.instance.PlaySFX("PopClick");
        yield return new WaitForSeconds(0.1f);
        LevelManager.instance.ChangeScene(sceneName);
    }
}
