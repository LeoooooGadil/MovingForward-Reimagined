using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneryCard : MonoBehaviour, IPointerUpHandler
{
    public string cardName { get; set;}
    public string sceneName { get; set; }
    public Texture2D thumbnail { get; set; }

    public bool isLocked { get; set; }
    public bool isCurrent { get; set; }
    public int cardIndex { get; set; }

    public TMP_Text cardNameText;
    public TMP_Text cardStatusText;
    public Image thumbnailImage;
    public Image frontImage;

    private SceneryManager sceneryManager;
    private Button button;
    private ButtonAnimator buttonAnimator;

    void Awake() {
        sceneryManager = SceneryManager.instance;
        button = GetComponent<Button>();
        buttonAnimator = GetComponent<ButtonAnimator>();
        thumbnailImage = GetComponent<Image>();

        if (sceneryManager == null) {
            Debug.LogError("SceneryManager is null");
        }
    }

    void Start() {
        if(buttonAnimator != null)
            buttonAnimator.isActive = !isCurrent || !isLocked;

        cardNameText.text = cardName;
        cardStatusText.text = isLocked ? "Locked" : isCurrent ? "Current" : "";
        button.interactable = !isCurrent;
        thumbnailImage.sprite = Sprite.Create(thumbnail, new Rect(0, 0, thumbnail.width, thumbnail.height), Vector2.zero);
        // set the front image color to R:200 G:200 B:200 A:255 if the card is Current or Locked
        frontImage.color = isCurrent || isLocked ? new Color32(200, 200, 200, 255) : Color.white;

        button.onClick.AddListener(() => StartCoroutine(OnClick()));
    }

	IEnumerator OnClick()
	{
        AudioManager.instance.PlaySFX("AcceptClick");
        sceneryManager.SetScenery(sceneName);

        yield return new WaitForSeconds(0.05f);
        AudioManager.instance.PlaySFX("CloseClick");
        LevelManager.instance.RemoveScene("Menu");

	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if(isCurrent || isLocked) {
            AudioManager.instance.PlaySFX("EhhEhhClick");
        }
	}
}
