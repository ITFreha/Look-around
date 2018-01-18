using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitHandler : MonoBehaviour {
	[SerializeField] private GameObject[] ObjsToHandleHit;
	[SerializeField] private GameObject menu;

	private GameObject lastHittedObj;
	private int state;


	void OnSelect() {
		if (lastHittedObj == menu)
			Messenger<string>.Broadcast (Events.loadLevel, "Menu");

		for (int i = 0; i < ObjsToHandleHit.Length; i++)
			if (ObjsToHandleHit [i] == lastHittedObj) {
				if (i == state + 1) {
					Messenger<UpdateProgress.Act>.Broadcast (Events.doEffect, UpdateProgress.Act.Success);
					state++;
				} else {
					Messenger<UpdateProgress.Act>.Broadcast (Events.doEffect, UpdateProgress.Act.Incorrect);
					state = -1;
				}
				break;
			}

		if (state == ObjsToHandleHit.Length - 1)
			Messenger.Broadcast (Events.onPassLevel);
	}

	void HandleHit(RaycastHit hit) {
		if (hit.transform.gameObject == menu) {
			Messenger.Broadcast (Events.fillPanel);
		}

		for (int i = 0; i < ObjsToHandleHit.Length; i++)
			if (hit.transform.gameObject == ObjsToHandleHit [i]) {

				if (hit.transform.gameObject == lastHittedObj)
					Messenger.Broadcast (Events.fillPanel);

				break;
			}

		if (hit.transform.gameObject != lastHittedObj) {
			Messenger.Broadcast (Events.clearPanel);
			lastHittedObj = hit.transform.gameObject;
		}
	}

	void Start () {
		state = -1;
	}

	void Update () {
		
	}

	void Awake() {
		Messenger<RaycastHit>.AddListener(Events.onHit, HandleHit);
		Messenger.AddListener (Events.onSelect, OnSelect);
	}

	void OnDestroy() {
		Messenger<RaycastHit>.RemoveListener(Events.onHit, HandleHit);
		Messenger.RemoveListener (Events.onSelect, OnSelect);
	}
}