using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera myCamera;
    private Rigidbody2D rb;
    public GameObject bullet;
    private LayerMask bulletMask;
    private SpriteRenderer spriteRenderer;

    public float force;
    private float timer = 0;
    public float timeToDestroy;

    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        bullet.GetComponent<PlayerBullet>();
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = myCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeToDestroy)
        {
            Debug.Log("Bullet destoyed");
            Destroy(gameObject);
        }
    }

    private void CheckCollision(GameObject gameObject, bool state)
    {
        if (bulletMask == (bulletMask | (1 << gameObject.layer)))
        {
            spriteRenderer.enabled = state;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CheckCollision(other.gameObject, state: true);
        var enemyHp = other.GetComponent<EnemyController>();
        Debug.Log("trigger entered");

        if (enemyHp != null)
        {
            DamageToEnemy(enemyHp);
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CheckCollision(other.gameObject, state: false);
        Debug.Log("trigger exited");
    }

    public void DamageToEnemy(EnemyController enemyHp)
    {
        enemyHp.health -= damage;
        Debug.Log($"Damage = {damage}");
        Debug.Log($"Enemy health = {enemyHp.health}");
    }
}
