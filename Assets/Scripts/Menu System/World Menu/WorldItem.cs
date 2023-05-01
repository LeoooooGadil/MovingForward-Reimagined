using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour, IPointerClickHandler
{
	public WorldManager worldManager;
	public string worldName;
	public string worldSceneName;
    public int worldIndex;
	public bool isCurrentWorld = false;
	public RawImage worldImage;
	public Text worldNameText;
	private Image image;

	private float animSpeed = 15f;

	void Start()
	{
		image = GetComponent<Image>();
	}

	void Update()
	{
        isCurrentWorld = (worldManager.currentWorld == worldIndex);

		if (!isCurrentWorld)
		{
			image.color = new Color(
				Mathf.Lerp(image.color.r, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.g, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.b, 1f, Time.deltaTime * animSpeed),
				1f);

			worldImage.color = new Color(
				Mathf.Lerp(worldImage.color.r, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(worldImage.color.g, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(worldImage.color.b, 1f, Time.deltaTime * animSpeed),
				1f);

			worldNameText.color = new Color(
				Mathf.Lerp(worldNameText.color.r, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(worldNameText.color.g, 1f, Time.deltaTime * animSpeed),
				Mathf.Lerp(worldNameText.color.b, 1f, Time.deltaTime * animSpeed),
				1f);
		}
		else
		{
			image.color = new Color(
				Mathf.Lerp(image.color.r, 0.3f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.g, 0.3f, Time.deltaTime * animSpeed),
				Mathf.Lerp(image.color.b, 0.3f, Time.deltaTime * animSpeed),
				1f);

			worldImage.color = new Color(
				Mathf.Lerp(worldImage.color.r, 0.5f, Time.deltaTime * animSpeed),
				Mathf.Lerp(worldImage.color.g, 0.5f, Time.deltaTime * animSpeed),
				Mathf.Lerp(worldImage.color.b, 0.5f, Time.deltaTime * animSpeed),
				1f);

			worldNameText.color = new Color(
				Mathf.Lerp(worldNameText.color.r, 0.7f, Time.deltaTime * animSpeed),
				Mathf.Lerp(worldNameText.color.g, 0.7f, Time.deltaTime * animSpeed),
				Mathf.Lerp(worldNameText.color.b, 0.7f, Time.deltaTime * animSpeed),
				1f);
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (isCurrentWorld)
		{
            AudioManager.instance.PlaySFX("EhhEhhClick");
            return;
		}

		StartCoroutine(ChangeScenery());
	}

	IEnumerator ChangeScenery()
	{
		AudioManager.instance.PlaySFX("AcceptClick");
		SceneryManager.instance.SetScenery(worldSceneName);

		yield return new WaitForSeconds(0.05f);
		AudioManager.instance.PlaySFX("CloseClick");
		LevelManager.instance.RemoveScene("Menu");
	}
}
