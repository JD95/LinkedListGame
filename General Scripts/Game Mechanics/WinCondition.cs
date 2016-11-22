using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public abstract class WinCondition : MonoBehaviour {

    public Text instructions;

    public GameBoard board;

    int stage = -1; // Because we want to progress to stage 0
    protected List<Stage> stages;

    private void setInstructionText(string text)
    {
        instructions.text = text;
    }

    public bool canProgress()
    {
        return (stage >= stages.Count) ? false : stages[stage].progressCheck();
    }

    public void progress()
    {
        stage++;
        setInstructionText(stages[stage].setup());
        GameBoard.log.wipeAll();
    }

    public bool win()
    {
        return stage == stages.Count - 1;
    }
}
