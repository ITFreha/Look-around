using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour {
	private GameObject cameraContainer;
	private Quaternion rot;

	private Gyroscope gyro;

	void Start () {
		cameraContainer = new GameObject ("Camera Container");
		cameraContainer.transform.position = transform.position;
		transform.SetParent (cameraContainer.transform);
		transform.rotation = Quaternion.Euler (90f, 90f, 0f);

		gyro = Input.gyro;
		gyro.enabled = true;

		cameraContainer.transform.rotation = Quaternion.Euler (90f, 90f, 0f);
		rot = new Quaternion (0, 0, 1, 0);
	}

	void Update () {
		transform.localRotation = gyro.attitude * rot;
	}
}
