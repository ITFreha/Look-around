using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
	[SerializeField] private GameObject congratulations;

	void Update () {
		
	}

	void OnPassLevel() {
		congratulations.SetActive (true);

		StartCoroutine (LoadMenu ());
	}

	IEnumerator LoadMenu() {
		yield return new WaitForSeconds (2);

		Messenger<string>.Broadcast (Events.loadLevel, "Menu");
	}

	void Awake() {
		Messenger.AddListener (Events.onPassLevel, OnPassLevel);
	}

	void OnDestroy() {
		Messenger.RemoveListener (Events.onPassLevel, OnPassLevel);
	}
}