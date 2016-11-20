using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Tuple<X, Y>
{
    public X first;
    public Y second;

    public Tuple(X first, Y second)
    {
        this.first = first;
        this.second = second;
    }
}

