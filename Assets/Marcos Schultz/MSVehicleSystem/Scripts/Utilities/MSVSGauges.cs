using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSVSGauges : MonoBehaviour {

	Transform background;
	Transform speedPointer;
	Transform rpmPointer;
	Transform fuelPointer;
	Text kmhText;
	Text mphText;
	Text rpmText;
	Text gearText;
	Text damageText;
	Text autoGearsText;
	Image handBrakeUI;
	Image engineUI;

	Vector3 startEulerAnglesSpeedPointer;
	Vector3 startEulerAnglesRpmPointer;
	Vector3 startEulerAnglesFuelPointer;
	RectTransform backgroundRect;

	//public variables
	[HideInInspector]
	public int vehicleKmh;
	[HideInInspector]
	public float vehiclePercentkmh;
	[HideInInspector]
	public float vehiclePercentrpm;
	[HideInInspector]
	public int vehicleCurrentRpm;
	[HideInInspector]
	public int vehicleCurrentGear;
	[HideInInspector]
	public float vehiclePercentDamage;
	[HideInInspector]
	public float vehiclePercentFuel;
	[HideInInspector]
	public bool handBrakeTrue;
	[HideInInspector]
	public bool enabledGauge;
	[HideInInspector]
	public float uiScaler = 1;
	[HideInInspector]
	public float uiOffset = 0;
	[HideInInspector]
	public bool enableDamageText;
	[HideInInspector]
	public bool autoGears;

	//
	float timer = 0;

	void Start () {
		//find background
		background = this.transform.Find ("background").transform;
		backgroundRect = background.GetComponent<RectTransform> ();
		//find pointers
		speedPointer = this.transform.Find ("background/speed/pointer").transform;
		rpmPointer = this.transform.Find ("background/rpm/pointer").transform;
		fuelPointer = this.transform.Find ("background/fuel/pointer").transform;
		//find Text
		kmhText = this.transform.Find ("background/kmhText").GetComponent<Text> ();
		mphText = this.transform.Find ("background/speed/mphText").GetComponent<Text> ();
		rpmText = this.transform.Find ("background/rpmText").GetComponent<Text> ();
		gearText = this.transform.Find ("background/rpm/gearText").GetComponent<Text> ();
		damageText = this.transform.Find ("background/damageText").GetComponent<Text> ();
		autoGearsText = this.transform.Find ("background/rpm/autoGearsText").GetComponent<Text> ();
		//find Image
		handBrakeUI = this.transform.Find ("background/handBrake").GetComponent<Image> ();
		engineUI = this.transform.Find ("background/engine").GetComponent<Image> ();

		//set values
		if (speedPointer) {
			startEulerAnglesSpeedPointer = speedPointer.localEulerAngles;
		}
		if (rpmPointer) {
			startEulerAnglesRpmPointer = rpmPointer.localEulerAngles;
		}
		if (fuelPointer) {
			startEulerAnglesFuelPointer = fuelPointer.localEulerAngles;
		}
	}

	void Update () {
		if (enabledGauge) {
			//enable UI
			if (!background.gameObject.activeInHierarchy) {
				background.gameObject.SetActive (true);
			}

			//UI scale
			uiScaler = Mathf.Clamp(uiScaler, 0.5f, 1.5f);
			if (background.localScale.x != uiScaler) {
				background.localScale = new Vector3 (uiScaler, uiScaler, 1);
				backgroundRect.anchoredPosition3D = new Vector3 (0, 80 * uiScaler, 0);
			}

			//UI Offset
			if (backgroundRect.anchoredPosition.x != (0.5 + uiOffset)) {
				backgroundRect.anchorMin = new Vector2 (0.5f + uiOffset, 0);
				backgroundRect.anchorMax = new Vector2 (0.5f + uiOffset, 0);
				backgroundRect.anchoredPosition3D = new Vector3 (0, 80 * uiScaler, 0);
			}

			//clamp
			vehicleKmh = Mathf.Clamp(vehicleKmh, 0, 420);
			vehicleCurrentRpm = Mathf.Clamp (vehicleCurrentRpm, 0, 9000);
			vehicleCurrentGear = Mathf.Clamp (vehicleCurrentGear, -1, 12);
			vehiclePercentDamage = Mathf.Clamp01 (vehiclePercentDamage);
			vehiclePercentFuel = Mathf.Clamp01 (vehiclePercentFuel);
			vehiclePercentkmh = Mathf.Clamp01 (vehiclePercentkmh);
			vehiclePercentrpm = Mathf.Clamp01 (vehiclePercentrpm);

			//set Text
			timer += Time.deltaTime;
			if (timer > 0.1f) { //10Hz is sufficient
				timer = 0;
				kmhText.text = vehicleKmh + " km/h";
				mphText.text = ((int)(vehicleKmh * 0.621371f)).ToString ();
				rpmText.text = vehicleCurrentRpm + " rpm";
				if (vehicleCurrentGear >= 0) {
					gearText.text = "D" + vehicleCurrentGear;
				} else {
					gearText.text = "R";
				}
				if (enableDamageText) {
					if (!damageText.gameObject.activeInHierarchy) {
						damageText.enabled = true;
					}
					damageText.text = "Damage: " + (int)(vehiclePercentDamage * 100) + "%";
				} else {
					if (damageText.gameObject.activeInHierarchy) {
						damageText.enabled = false;
					}
				}
				autoGearsText.enabled = autoGears;
			}

			//set Image
			handBrakeUI.enabled = handBrakeTrue;
			engineUI.enabled = (vehiclePercentDamage > 0.5f);

			//set Pointers
			speedPointer.transform.localEulerAngles = new Vector3 (startEulerAnglesSpeedPointer.x, startEulerAnglesSpeedPointer.y, startEulerAnglesSpeedPointer.z - vehiclePercentkmh * 270.0f);
			rpmPointer.transform.localEulerAngles = new Vector3 (startEulerAnglesRpmPointer.x, startEulerAnglesRpmPointer.y, startEulerAnglesRpmPointer.z - vehiclePercentrpm * 270.0f);
			fuelPointer.transform.localEulerAngles = new Vector3 (startEulerAnglesFuelPointer.x, startEulerAnglesFuelPointer.y, startEulerAnglesFuelPointer.z + vehiclePercentFuel * 135.0f);

		} else {
			if (background.gameObject.activeInHierarchy) {
				background.gameObject.SetActive (false);
			}
		}
	}
}
