using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float groundDistance;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castPosition = transform.position;
        castPosition.y += 1;
        if (Physics.Raycast(castPosition, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePosition = transform.position;
                movePosition.y = hit.point.y + groundDistance;
                transform.position = movePosition;
            }
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(x, 0, y);
        rb.linearVelocity = moveDirection * speed;

        if (x != 0 && x < 0)
        {
            sr.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            sr.flipX = false;
        }

    }
}
