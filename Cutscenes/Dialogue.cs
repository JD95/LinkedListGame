using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Dialogue : MonoBehaviour {

    public Text textbox;

    public string[] dialogue;
    public AudioSource[] voiceCues;
    public float[] lineDuration;
    public int line = 0; // Which line of dialogue we are on

    public bool typing = false;
    public float typing_delay;

    private float type_counter = 0;
    private float line_counter = 0;
    private int string_index = 0;

	// Use this for initialization
	void Start () {
        textbox.text = "";
        if (voiceCues.Length > line) voiceCues[line].Play();
    }
	
	// Update is called once per frame
	void Update () {
        type_counter += Time.deltaTime * 10;
        line_counter += Time.deltaTime;

        if (typing && type_counter >= typing_delay)
        {
            type_counter = 0;
            textbox.text += dialogue[line][string_index++];
            if (dialogue[line].Length == string_index) typing = false;
        }

        if (lineDuration.Length > line && line_counter >= lineDuration[line]) next_line();
	}

    void next_line()
    {
        line++;
        typing = true;
        string_index = 0;
        line_counter = 0;
        type_counter = 0;
        textbox.text = "";
        if (voiceCues.Length > line) voiceCues[line].Play();
    }
}
