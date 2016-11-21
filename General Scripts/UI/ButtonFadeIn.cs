using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonFadeIn : MonoBehaviour {

    bool active = false;
    public Button button;


	// Use this for initialization
	void Start () {
        updateButtonNormalColor(0);
    }

    void updateButtonNormalColor(float f)
    {
        var newColorBlock = button.colors;
        var newColor = button.colors.normalColor;
        newColor.a = f;
        newColorBlock.normalColor = newColor;
        button.colors = newColorBlock;
    }
	
	// Update is called once per frame
	void Update () {
        if (active && button.colors.normalColor.a < 255)
        {
            updateButtonNormalColor(button.colors.normalColor.a + Time.deltaTime * 1);
        }
	}

    public void activate()
    {
        active = true;
    }
}
