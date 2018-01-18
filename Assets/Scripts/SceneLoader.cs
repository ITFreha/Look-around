using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
	[SerializeField] private CanvasRenderer panel;
	public float reverseblackoutTime = 0.015f;
	public float blackoutTime = 0.02f;

	private string sceneToLoad;

	void RunLevel(string name) {
		sceneToLoad = name;

		StartCoroutine (Blackout (false));
	}

	IEnumerator Blackout(bool reverse) {
		panel.gameObject.SetActive (true);

		if (reverse) {
			for (int i = 30; i >= 0; i--) {
				panel.SetAlpha ((float)1 / 30 * i);

				yield return new WaitForSeconds (reverseblackoutTime);
			}
			panel.gameObject.SetActive (false);
		} else {
			for (int i = 0; i <= 30; i++) {
				panel.SetAlpha ((float)1 / 30 * i);

				yield return new WaitForSeconds (blackoutTime);
			}
			SceneManager.LoadScene (sceneToLoad);
		}
	}

	void Start () {
		StartCoroutine (Blackout (true));
	}

	void Awake() {
		Messenger<string>.AddListener (Events.loadLevel, RunLevel);
	}

	void OnDestroy() {
		Messenger<string>.RemoveListener (Events.loadLevel, RunLevel);
	}
}