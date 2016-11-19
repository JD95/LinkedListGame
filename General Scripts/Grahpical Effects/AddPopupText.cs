using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AddPopupText : MonoBehaviour {

	public GameObject canvas;
	Queue<GameObject> popups = new Queue<GameObject>();


	// Use this for initialization
	void Start () {
	}

	public void makePopup(string message)
	{
		GameObject newText = Instantiate (Resources.Load("Popup Text")) as GameObject;
		newText.transform.SetParent (canvas.transform, false);
		newText.GetComponent<Text> ().text = message;
		popups.Enqueue (newText);
	}

	// Update is called once per frame
	void Update () {

		if (popups.Count == 0)
			return;
		
		foreach (var popup in popups){
			var oldColor = popup.GetComponent<Text> ().color;
			oldColor.a -= Time.deltaTime * 0.6f;

			popup.GetComponent<Text> ().color = oldColor;
			popup.transform.position = popup.transform.position + new Vector3 (0, -1, 0);
		}

		if (popups.Peek ().GetComponent<Text> ().color.a <= 0) {
			GameObject.Destroy (popups.Dequeue ());
		}

	}
}
 