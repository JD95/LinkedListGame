using UnityEngine;
using System.Collections.Generic;
using System;

public class FreePlayLogic : WinCondition {

    // Use this for initialization
    void Start () {
        stages = new List<Stage>();
        progress();
	}
}
