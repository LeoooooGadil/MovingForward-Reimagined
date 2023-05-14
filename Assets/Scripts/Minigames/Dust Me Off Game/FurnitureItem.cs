using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureItem : MonoBehaviour
{
	public List<Sprite> furnitureSprites = new List<Sprite>();
	public FurnitureDustBar dustBar;
	public GameObject dustParticle;

	// 0 = spawn from left
	// 1 = spawn from Top
	// 2 = spawn from right
	// 3 = spawn from bottom
	public int whereDidSpawn = 0;
	public float dustAmount = 100;
	public float negativeDustAmount = 10;
	public float points = 10;
	public DustMeOffGame dustMeOffGame;
	private SpriteRenderer spriteRenderer;

	public Color dustedOffColor;
	public Color dustedOnColor;

	private float speed = 1.5f;

	public bool isStopMoving = false;
	public bool isActive = true;

	void Start()
	{
		AudioManager.instance.PlaySFX("ButtonClick");
		int maxDust = Random.Range(100, 500);
		int negateDust = Random.Range(10, 25) * (maxDust / 100);
		dustBar.UpdateDustAmountMax(maxDust);
		negativeDustAmount = negateDust;
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = furnitureSprites[Random.Range(0, furnitureSprites.Count)];
		spriteRenderer.color = dustedOnColor;

		points = CalculatePoints(maxDust);

		// speed is ranging from 1.5 to 3.0 based on the dust amount
		speed = 1.5f + (maxDust / 100);

		dustMeOffGame.AddFurnitureItem(this);
	}

	float CalculatePoints(int maxDust)
	{
		// calculate points based on dust amount and negative dust amount (how much dust is removed per click)
		// the higher the negative dust amount, the less points you get
		// the max points you can get is 50
		// the lowest points you can get is 5
		// points cannot be negative

		points = (maxDust / 100) * (50 - (negativeDustAmount / 2));
		if (points < 5)
		{
			points = 5;
		}
		return Mathf.Round(points);
	}

	private void OnMouseDown()
	{
		if (isActive == false)
		{
			return;
		}

		Debug.Log("Dusting Off");
		AudioManager.instance.PlaySFX("PloukSfx");
		dustBar.UpdateDustAmount(negativeDustAmount);
		spriteRenderer.color = Color.Lerp(dustedOffColor, dustedOnColor, dustBar.slider.value / dustBar.slider.maxValue);

		if (dustBar.slider.value <= 0)
		{
			Debug.Log("Dust Bar is empty");
			AudioManager.instance.PlaySFX("WinSfx");
			dustMeOffGame.CleanedFurniture(transform.position, points);
			dustMeOffGame.NegateSpawnRate();
			GameObject dust = Instantiate(dustParticle, transform.position, Quaternion.identity);
			Destroy(dust, 1f);
			Destroy(gameObject);
		}
		else
		{
			GameObject dust = Instantiate(dustParticle, transform.position, Quaternion.identity);
			// set parent to this object
			dust.transform.SetParent(transform);
			Destroy(dust, 1f);
		}
	}

	public void StopThisObject()
	{
		isStopMoving = true;
		isActive = false;
	}


	// this object will spawn offscreen and moves towards the opposite side of the screen
	// so that it appears to be moving across the screen
	// once it reaches the opposite side of the screen, it will be destroyed

	void Update()
	{
		if (whereDidSpawn == 0 && isStopMoving == false)
		{
			transform.Translate(Vector3.right * Time.deltaTime * speed);
		}
		else if (whereDidSpawn == 1 && isStopMoving == false)
		{
			transform.Translate(Vector3.down * Time.deltaTime * speed);

		}
		else if (whereDidSpawn == 2 && isStopMoving == false)
		{
			transform.Translate(Vector3.left * Time.deltaTime * speed);
		}
		else if (whereDidSpawn == 3 && isStopMoving == false)
		{
			transform.Translate(Vector3.up * Time.deltaTime * speed);
		}

		// destroy object if it goes offscreen (left, right, top, bottom)
		// check if object comes from left then destroy if it goes offscreen to the right
		if (whereDidSpawn == 0 && transform.position.x > 12)
		{
			AudioManager.instance.PlaySFX("WrongSfx");
			dustMeOffGame.NegateSpawnRate();
			dustMeOffGame.UncleanedFurniture(transform.position, points);
			Destroy(gameObject);
		}

		// check if object comes from top then destroy if it goes offscreen to the bottom
		if (whereDidSpawn == 1 && transform.position.y < -7)
		{
			AudioManager.instance.PlaySFX("WrongSfx");
			dustMeOffGame.NegateSpawnRate();
			dustMeOffGame.UncleanedFurniture(transform.position, points);
			Destroy(gameObject);
		}

		// check if object comes from right then destroy if it goes offscreen to the left
		if (whereDidSpawn == 2 && transform.position.x < -12)
		{
			AudioManager.instance.PlaySFX("WrongSfx");
			dustMeOffGame.NegateSpawnRate();
			dustMeOffGame.UncleanedFurniture(transform.position, points);
			Destroy(gameObject);
		}

		// check if object comes from bottom then destroy if it goes offscreen to the top
		if (whereDidSpawn == 3 && transform.position.y > 7)
		{
			AudioManager.instance.PlaySFX("WrongSfx");
			dustMeOffGame.NegateSpawnRate();
			dustMeOffGame.UncleanedFurniture(transform.position, points);
			Destroy(gameObject);
		}
	}

	private void OnDestroy()
	{
		dustMeOffGame.RemoveFurnitureItem(this);
	}
}
