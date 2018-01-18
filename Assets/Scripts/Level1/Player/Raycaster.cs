using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Raycaster : MonoBehaviour {
	private float maxRayDistance;
	private Camera cam;

	void Start () {
		maxRayDistance = 10 * Mathf.Sqrt (3);

		cam = this.GetComponent<Camera> ();
	}

	void Update () {
		RaycastHit hit;

		Vector3 point = new Vector3 (
			cam.pixelWidth/2,
			cam.pixelHeight/2, 0);
		
		Ray ray = cam.ScreenPointToRay (point);

		if (Physics.Raycast (ray, out hit, maxRayDistance)) {
			Messenger<RaycastHit>.Broadcast (Events.onHit, hit);
		}
	}
}