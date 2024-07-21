using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    public Transform rotationPoint;
    public Transform aim;

    public GameObject bullet;
    private Vector3 mousePos;
    public Camera myCamera;
    public bool canFire;
    private float timer = 0;
    public float timeBetweenShots;

    // Start is called before the first frame update
    void Start()
    {
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        CanAttack();
    }

    private void LookAround()
    {
        // moves rotationPoint
        mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - rotationPoint.transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        rotationPoint.transform.rotation = UnityEngine.Quaternion.Euler(0, 0, rotZ);
    }

    private void Attack()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, aim.position, Quaternion.identity);
        }
    }

    private void CanAttack()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenShots)
            {
                canFire = true;
                timer = 0;
            }
        }
        else
        {
            Attack();
        }
    }
}
