using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Transform rotationPoint;
    public Transform aim;

    public float speed;
    public Camera myCamera;
    private Vector3 mousePos;
    private Vector3 oldPos;
    public bool isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (player == null)
        {
            Debug.Log("No Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position != oldPos)
        {
            isMoving = true;
            oldPos = player.transform.position;
        }
        else
        {
            isMoving = false;
        }

        if (!isMoving)
        {
            CameraToMouse();
        }
        else
        {
            CameraToPlayer();
        }
    }

    private void CameraToMouse()
    {
        // moves rotationPoint
        mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - rotationPoint.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotationPoint.transform.rotation = UnityEngine.Quaternion.Euler(0,0,rotZ);

        // move camera to position
        float delay = speed * Time.deltaTime;
        Vector3 position = myCamera.transform.position;
        position.y = Mathf.Lerp(transform.position.y, aim.position.y, delay);
        position.x = Mathf.Lerp(transform.position.x, aim.position.x, delay);
        position.z = -10;
        myCamera.transform.position = position;
    }

    private void CameraToPlayer()
    {
        float delay = speed * Time.deltaTime;
        Vector3 position = myCamera.transform.position;
        position.y = Mathf.Lerp(transform.position.y, player.position.y, delay);
        position.x = Mathf.Lerp(transform.position.x, player.position.x, delay);
        position.z = -10;
        myCamera.transform.position = position;
    }
}
