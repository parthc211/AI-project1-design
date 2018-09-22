using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform followTarget;
    public Vector3 targetOffset;
    public float moveSpeed;
    public float scrollSpeed;
    public float zoomAmount;
    public float zoomInMax;
    public float zoomOutMax;

    private Camera mainCam;

    private void Start()
    {
        mainCam = GetComponent<Camera>();
    }

    private void Update()
    {
        zoomAmount -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        zoomAmount = Mathf.Clamp(zoomAmount, zoomInMax, zoomOutMax);

        mainCam.orthographicSize = zoomAmount;
    }

    private void LateUpdate()
    {
        if(followTarget != null)
        {

            transform.position = Vector3.Lerp(transform.position, followTarget.position + targetOffset, moveSpeed * Time.deltaTime);
        }
    }
}
