using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static IEnumerator LateAction(Action action)
    {
        yield return null;
        action();
    }
}
