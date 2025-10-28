using UnityEngine;
public class EnemyMovement : MonoBehaviour
{
    [Tooltip("The GameObject this enemy should follow.")]
    public GameObject target;

    [Tooltip("The movement speed of the enemy.")]
    public float moveSpeed = 5f;

    [Tooltip("The minimum distance to maintain from the target.")]
    public float stoppingDistance = 1f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
            if (target == null)
            {
                Debug.LogError("Player not found! Make sure your Player GameObject has the 'Player' tag.");
            }
        }
    }

    void Update()
    {
        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);

            if (distance > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    target.transform.position,
                    moveSpeed * Time.deltaTime
                );
            }
        }
    }
}
