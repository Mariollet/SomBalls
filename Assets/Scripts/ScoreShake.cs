using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreShake : MonoBehaviour {

	Vector3 scoreInitialPosition;
	Vector3 scorePopupInitialPosition;
	Vector3 secondScorePopupInitialPosition;
	private float shakeMagnetude = 0.03f, shakeTime = 0.1f;
	public Text scorePopupText;
	private Transform UI;
	private Text NewScorePopup;
	private Text SecondScorePopup;

	private void Start()
	{
		UI = GameObject.Find("UI Controller").transform;
		scoreInitialPosition = transform.position;
		scorePopupInitialPosition = new Vector3(0, -350, 0);
		secondScorePopupInitialPosition = new Vector3(0, 450, 0);
	}

	public void ShakeIt()
	{
		InvokeRepeating ("StartScoreShaking", 0f, 0.005f);
		Invoke ("StartScorePopupShaking", 0f);
		Invoke ("StopScoreShaking", shakeTime);
	}

	void StartScoreShaking()
	{
		float cameraShakingOffsetX = Random.value * shakeMagnetude * 4.5f - shakeMagnetude;
		float cameraShakingOffsetY = Random.value * shakeMagnetude * 4.5f - shakeMagnetude;
		Vector3 cameraIntermadiatePosition = transform.position;
		cameraIntermadiatePosition.x += cameraShakingOffsetX;
		cameraIntermadiatePosition.y += cameraShakingOffsetY;
		transform.position = cameraIntermadiatePosition;
	}

	void StartScorePopupShaking()
	{
		NewScorePopup = Instantiate(scorePopupText, scorePopupInitialPosition, Quaternion.identity);
		NewScorePopup.transform.SetParent(UI);
		NewScorePopup.transform.localPosition = scorePopupInitialPosition;
		float cameraShakingOffsetX = Random.value * shakeMagnetude * 60 - shakeMagnetude;
		float cameraShakingOffsetY = Random.value * shakeMagnetude * 60 - shakeMagnetude;
		Vector3 cameraIntermadiatePosition = NewScorePopup.transform.position;
		cameraIntermadiatePosition.x += cameraShakingOffsetX;
		cameraIntermadiatePosition.y += cameraShakingOffsetY;
		NewScorePopup.transform.position = cameraIntermadiatePosition;
		StartCoroutine(DestroyAfter(NewScorePopup));

		SecondScorePopup = Instantiate(scorePopupText, secondScorePopupInitialPosition, Quaternion.identity);
		SecondScorePopup.transform.SetParent(UI);
		SecondScorePopup.transform.localPosition = secondScorePopupInitialPosition;
		float secondCameraShakingOffsetX = Random.value * shakeMagnetude * 25 - shakeMagnetude;
		float secondCameraShakingOffsetY = Random.value * shakeMagnetude * 25 - shakeMagnetude;
		Vector3 secondcameraIntermadiatePosition = SecondScorePopup.transform.position;
		secondcameraIntermadiatePosition.x += secondCameraShakingOffsetX;
		secondcameraIntermadiatePosition.y += secondCameraShakingOffsetY;
		SecondScorePopup.transform.position = secondcameraIntermadiatePosition;
		StartCoroutine(DestroyAfter(SecondScorePopup));
	} 

	IEnumerator DestroyAfter(Text NewScorePopup)
	{
		yield return new WaitForSeconds(2f);
		Destroy(NewScorePopup.gameObject);
	}
	void StopScoreShaking()
	{
		CancelInvoke("StartScoreShaking");
		transform.position = scoreInitialPosition;
	}

}
