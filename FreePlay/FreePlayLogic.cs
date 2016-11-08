using UnityEngine;
using System.Collections;
using System;

public class FreePlayLogic : WinCondition {

    public override bool canProgress()
    {
        return false;
    }

    public override void progress()
    {
    }

    public override bool win()
    {
        return false;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
