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

    public static float NormalizeAngle(float angle)
    {
        if (angle < -180f)
            angle += 360f;
        else if (angle > 180f)
            angle -= 360f;
        return angle;
    }
}
