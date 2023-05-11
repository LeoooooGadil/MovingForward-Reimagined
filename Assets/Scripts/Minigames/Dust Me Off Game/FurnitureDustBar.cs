using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnitureDustBar : MonoBehaviour
{
	public Slider slider;
    public new Camera camera;
    public Transform target;
    public Vector3 offset;
    public Text dustAmountText;

	void Start()
	{
		slider = GetComponent<Slider>();
        target = GetComponentInParent<FurnitureItem>().gameObject.transform;
    }

	// void Update()
	// {
    //     transform.rotation = camera.transform.rotation;
    //     transform.position = target.position + offset;
	// }

    public void UpdateDustAmount(float dustAmount)
    {
        slider.value -= dustAmount;
        dustAmountText.text = slider.value.ToString("F0");
    }

    public void UpdateDustAmountMax(float dustAmountMax)
    {
        slider.maxValue = dustAmountMax;
        slider.value = dustAmountMax;
        dustAmountText.text = slider.value.ToString("F0");
    }
}
