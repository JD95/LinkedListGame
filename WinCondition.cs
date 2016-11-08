using UnityEngine;
using System.Collections;

public abstract class WinCondition : MonoBehaviour {
    public abstract bool win();
    public abstract bool canProgress();
    public abstract void progress();
}
