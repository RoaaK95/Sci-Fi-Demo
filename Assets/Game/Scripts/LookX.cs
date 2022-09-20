using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookX : MonoBehaviour
{
    [SerializeField]
    private float _sensitivity = 0.5f;
    void Update()
    {
        float _mouseX = Input.GetAxis("Mouse X");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += _mouseX * _sensitivity;
        transform.localEulerAngles = newRotation;


    }
}
