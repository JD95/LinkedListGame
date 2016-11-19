using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class ZoomPerspective : MonoBehaviour {

    public Camera cam;

    public bool active = false;
    public float warpRate;

    public float transitionAfter;

    private float time_counter = 0;

    public string nextScene;

	// Use this for initialization
	void Start () {
	
	}
	
    public void activate()
    {
        active = true;
    }

	// Update is called once per frame
	void Update () {
        if (active)
        {
            cam.fieldOfView += Time.deltaTime * warpRate;

            time_counter += Time.deltaTime;

            if (time_counter >= transitionAfter) SceneManager.LoadScene(nextScene);
        }
	}
}
