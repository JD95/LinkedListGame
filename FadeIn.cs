using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {

    public Material plane;
    public float faderate = 10;

    private float countdown = 255;

	// Use this for initialization
	void Start () {
        var oldColor = plane.color;
        oldColor.a = 1;
        plane.color = oldColor;
    }
	
	// Update is called once per frame
	void Update () {
        if (countdown > 0)
        {
            var oldColor = plane.color;
            var time = Time.deltaTime * faderate;
            oldColor.a -= time;
            countdown -= time;
            plane.color = oldColor;
        }
	}
}
