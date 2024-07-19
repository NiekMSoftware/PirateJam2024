using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.Log("No Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float delay = speed * Time.deltaTime;
        Vector3 position = transform.position;
        position.y = Mathf.Lerp(transform.position.y, player.position.y, delay);
        position.x = Mathf.Lerp(transform.position.x, player.position.x, delay);
        position.z = -10;
        transform.position = position;
    }
}
