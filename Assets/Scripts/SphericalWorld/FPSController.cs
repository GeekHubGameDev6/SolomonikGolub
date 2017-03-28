using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    /// <summary>
    /// Ended on 15:11 
    /// </summary>
    /// 
    public float mouseSensivityX = 250f;
    public float mouseSensivityY = 250f;

    private Transform cameraT;
    private float verticalLookRotation;


    private void Start()
    {
        cameraT = Camera.main.transform;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X"));
        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -60, 60);
        cameraT.localEulerAngles = Vector3.left * verticalLookRotation;
    }
}
