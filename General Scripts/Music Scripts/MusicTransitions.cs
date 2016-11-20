using UnityEngine;
using System.Collections;

public class MusicTransitions : MonoBehaviour {

    public AudioSource intro;
    public AudioSource[] loops;
    public AudioSource outro;

    public int current_loop = 0;
    public bool looping = false;
    public float time_to_loop;

    private float time_counter = 0;

	// Use this for initialization
	void Start () {
        time_to_loop = intro.clip.length;
	}
	
	// Update is called once per frame
	void Update () {

        time_counter += Time.deltaTime;

        if (!looping && time_counter >= time_to_loop)
        {
            intro.Stop();
            loops[current_loop].Play();
            looping = true;
        }
	}

    public void playOutro()
    {
        intro.Stop();
        loops[current_loop].Stop();
        outro.Play();
    }
}
