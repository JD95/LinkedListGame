using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ProgressButton : MonoBehaviour {

    public bool levelComplete = false;
    public Text buttonText;
    public string nextLevel;
    public GameBoard board;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(!levelComplete && board.level.win())
        {
            buttonText.text = "Next Level";
            levelComplete = true;
        }
	}

    public void click()
    { 
        if (board.level.win())
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
