using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonBlink : MonoBehaviour {

	public Button button;
	public Color normalColor;
	public Color blinkColor;
	public bool blink = false;
	public float period;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (blink == true) {
			//  slowly interpolate to and from blink color
			var oldcolors = button.colors;
			oldcolors.normalColor = Color.Lerp(Color.yellow, blinkColor, Mathf.Abs(Mathf.Sin(period*Time.time)));
			button.colors = oldcolors;
		} else {
			var oldcolors = button.colors;
			oldcolors.normalColor = normalColor;
			button.colors = oldcolors;
		}
	}
}
