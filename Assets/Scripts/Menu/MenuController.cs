using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	[SerializeField] private RectTransform scrollPanel;

	private Vector3 scrollPos;
	private float scrollFadeRate = 0.013f;
	private bool isFaded = false;
	private float k = 23.15f;

	public void Exit() {
		Application.Quit ();
	}

	public void ShowLevels() {
		if (!isFaded)
			StartCoroutine (FadeOut ());
	}

	public void HideLevels() {
		if (isFaded) {
			StartCoroutine (FadeIn ());
		}
	}

	void Start () {
		scrollPos = scrollPanel.localPosition;
	}

	void Update () {
		
	}
		
	float f(float x) {
		return 1 / Mathf.Sqrt (x) - 1;
	}

	IEnumerator FadeOut() {
		isFaded = true;

		for (int i = 30; i >= 1; i--) {
			scrollPos.x -= f ((float)1 / i) * k;
			scrollPanel.localPosition = scrollPos;

			yield return new WaitForSeconds (scrollFadeRate);
		}
	}

	IEnumerator FadeIn() {
		isFaded = false;

		for (int i = 30; i >= 1; i--) {
			scrollPos.x += f ((float)1 / i) * k;
			scrollPanel.localPosition = scrollPos;

			yield return new WaitForSeconds (scrollFadeRate);
		}
	}
}