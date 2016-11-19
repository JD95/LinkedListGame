using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class ZoomPerspective : MonoBehaviour {

    public Camera cam;
    public ParticleSystem particles;

    public bool active = false;
    public float warpRate;

    public float transitionAfter;

    private float time_counter = 0;

    public string nextScene;

	// Use this for initialization
	void Start () {
        particles.Stop();

    }
	
    public void activate()
    {
        active = true;
        particles.Play();
    }

	// Update is called once per frame
	void Update () {
        if (active)
        {
            var warp = Time.deltaTime * warpRate;
            cam.fieldOfView += warp;
            //transform.Rotate(new Vector3(warp * 0.5f, 0, 0));
            time_counter += Time.deltaTime;

            if (time_counter >= transitionAfter) SceneManager.LoadScene(nextScene);
        }
	}
}
