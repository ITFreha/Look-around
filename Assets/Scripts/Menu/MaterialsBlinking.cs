using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsBlinking : MonoBehaviour {
	[SerializeField] private Material startMat;
	[SerializeField] private Material endMat;

	private Renderer render;
	Color initColor;

	private float changingRate = 0.02f;

	void Start () {
		render = this.GetComponent<Renderer> ();
		initColor = render.material.GetColor ("_EmissionColor");

		StartCoroutine (DoBlinking ());
	}

	void Update () {
		/*if (Input.GetKey (KeyCode.Space)) {
			//Color color = new Color (15f, 13.639706f, 13.639706f);
			Color color = mat.GetColor("_EmissionColor") * 2f;
			//mat.SetColor ("_EmissionColor", color);
			DynamicGI.SetEmissive (this.GetComponent<Renderer> (), color);
		} else {
			DynamicGI.SetEmissive (this.GetComponent<Renderer> (), Color.black);
		}*/
	}

	IEnumerator DoBlinking() {
		int n = 60;

		while (true) {
			for (int i = 0; i <= n; i++) {
				DynamicGI.SetEmissive (render, initColor * (1 + (float)i / n / 2));

				yield return new WaitForSeconds (changingRate);
			}

			for (int i = n; i >= 0; i--) {
				DynamicGI.SetEmissive (render, initColor * (1 + (float)i / n / 2));

				yield return new WaitForSeconds (changingRate);
			}
		}

		//int EmissionID = Shader.PropertyToID ("_EmissionColor");

		/*for (int i = 0; i <= 20; i++) {
			

			yield return new WaitForSeconds (changingRate);
		}

		Color emisColor = mat.GetColor (EmissionID);
		emisColor.a = 500000;
		mat.SetColor ("_EmissionColor", emisColor);*/
	}
}