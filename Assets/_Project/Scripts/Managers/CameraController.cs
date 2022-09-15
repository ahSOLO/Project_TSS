using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float lerpSpeed;

    private void Update()
    {
        if (Mathf.Abs(transform.position.x - PlayerController.instance.GetPlayerPosition().x) > 0.1f 
            || Mathf.Abs(transform.position.y - PlayerController.instance.GetPlayerPosition().y) > 0.1f)
        {
            Vector3 lerpDestination = new Vector3(PlayerController.instance.GetPlayerPosition().x, PlayerController.instance.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, lerpDestination, lerpSpeed * Time.deltaTime);
        }
    }
}
