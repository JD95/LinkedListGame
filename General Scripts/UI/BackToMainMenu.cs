using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour {

    public string level;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void backToMainMenu()
    {
        SceneManager.LoadScene(level);
    }
}
