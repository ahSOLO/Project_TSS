using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform camTransform;
    [Range(1, 10)]public float parallaxFactor;

    private void Update()
    {
        var target = -camTransform.position * 0.01f * parallaxFactor;
        transform.localPosition = new Vector3(target.x, target.y, transform.localPosition.z);
    }
}
