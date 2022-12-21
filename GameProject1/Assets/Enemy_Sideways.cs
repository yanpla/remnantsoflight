using UnityEngine;
using System.Collections;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge)
            {
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = false;
        }
        else
        {
            if (transform.position.x < rightEdge)
            {
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
                movingLeft = true;
        }
    }

IEnumerator waiter()
{
    yield return new WaitForSeconds(1);
    Damaged = false;
}

    bool Damaged;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !Damaged)
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            Damaged = true;
            StartCoroutine(waiter());
        }
        
    }
}