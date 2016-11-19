using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Stage
{

    public Func<bool> progressCheck;
    public Func<string> setup;

    // Creates a new stage for the level
    public Stage(Func<string> setup, Func<bool> progressCondition)
    {
        this.progressCheck = progressCondition;
        this.setup = setup;
    }
}
