using UnityEngine;
using System.Collections;
using System;

public class ProgressMessages : MonoBehaviour {

    System.Random rand = new System.Random();

    public AudioSource[] errors;

    public int error_level = 0;

    private float time_since_last_error = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        time_since_last_error += Time.deltaTime;

        if (time_since_last_error > 5) error_level = 0;
	}

    public void stopOtherSounds()
    {
        foreach (var sound in errors)
        {
            sound.Stop();
        }
    }

    public void playError()
    {
        if(error_level <= 5)
        {
            stopOtherSounds();
            errors[rand.Next(0, 3)].Play();
            time_since_last_error = 0;
            error_level++;
        }
        else if(error_level > 5 && error_level < 8)
        {
            stopOtherSounds();
            errors[rand.Next(3, 7)].Play();
            time_since_last_error = 0;
            error_level++;
        }
        else
        {
            stopOtherSounds();
            errors[7].Play();
            time_since_last_error = 0;
            error_level = 0;
        }
    }
}
