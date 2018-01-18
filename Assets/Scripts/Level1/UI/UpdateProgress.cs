using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateProgress : MonoBehaviour {
	[SerializeField] private RectTransform fillingPanel;
	[SerializeField] private Text text;
	[SerializeField] private CanvasRenderer comprCR;

	private CanvasRenderer thisCR;
	private CanvasRenderer textCR;
	private float initialWidthFillPanel;
	private Color successColor;
	private Color incorrectColor;
	private Color initColor;
	private bool canDoSuccessEffect;

	public float rateOfFilling = 10f;
	public float rateOfIncreasing = 3f;

	public enum Act {
		Success, Incorrect
	}

	void SetOpacity(float opacity) {
		/*Color color = comprCR.GetColor ();
		color.a = opacity;
		comprCR.SetColor (color);*/

		comprCR.SetAlpha (opacity);

		/*color = thisCR.GetColor ();
		color.a = opacity;
		thisCR.SetColor (color);*/

		thisCR.SetAlpha (opacity);

		/*color = textCR.GetColor ();
		color.a = opacity;
		textCR.SetColor (color);*/

		textCR.SetAlpha (opacity);
	}

	void ClearPanel() {
		fillingPanel.SetSizeWithCurrentAnchors (
			RectTransform.Axis.Horizontal,
			0
		);

		comprCR.SetColor (initColor);
		SetOpacity (0);
		canDoSuccessEffect = true;

		this.gameObject.SetActive (false);
	}

	IEnumerator MakeEffect(Act act) {
		Color toUse = initColor;

		switch (act) {
		case Act.Incorrect:
			toUse = incorrectColor;
			break;
		case Act.Success:
			toUse = successColor;
			break;
		}

		for (int i = 0; i <= 18; i++) {
			comprCR.SetColor (Color.Lerp (
				initColor,
				toUse,
				(float)i / 18));
			
			yield return new WaitForSeconds (0.025f);
		}
	}

	void ChangePanelWidth() {
		this.gameObject.SetActive (true);

		if (fillingPanel.rect.size.x >= initialWidthFillPanel) {
			//text.text = "100%";

			if (canDoSuccessEffect) {
				canDoSuccessEffect = false;

				Messenger.Broadcast (Events.onSelect);
			}

		} else {
			fillingPanel.SetSizeWithCurrentAnchors (
				RectTransform.Axis.Horizontal,
				fillingPanel.rect.size.x + rateOfFilling * Time.deltaTime
			);

			SetOpacity (Mathf.Clamp01 (fillingPanel.rect.size.x / 100));

			//text.text = ((int)(fillingPanel.rect.size.x / initialWidthFillPanel * 100)).ToString () + "%";
		}
	}

	void DoEffect(Act act) {
		StartCoroutine (MakeEffect (act));
	}

	void Start() {
		successColor = new Color (0, 1, 0, (float) 220 / 255);
		incorrectColor = new Color (1, 0, 0, (float)300 / 255);
		initColor = comprCR.GetColor ();
		canDoSuccessEffect = true;

		initialWidthFillPanel = fillingPanel.rect.size.x;

		thisCR = this.GetComponent<CanvasRenderer> ();
		textCR = text.GetComponent<CanvasRenderer> ();

		SetOpacity (0);
	}

	void Awake() {
		Messenger.AddListener(Events.fillPanel, ChangePanelWidth);
		Messenger.AddListener(Events.clearPanel, ClearPanel);
		Messenger<Act>.AddListener (Events.doEffect, DoEffect);
	}

	void OnDestroy() {
		Messenger.RemoveListener(Events.fillPanel, ChangePanelWidth);
		Messenger.RemoveListener(Events.clearPanel, ClearPanel);
		Messenger<Act>.RemoveListener (Events.doEffect, DoEffect);
	}
}