using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyController : CircleController
{
    private GameObject CirclePlayer;
    public bool canMove;
    public GameObject RaycastCheck;
    private EnemyAttackController enemyAttackController;

    void Start()
    {
        CirclePlayer = GameObject.FindWithTag("Player");
        enemyAttackController = GetComponentInChildren<EnemyAttackController>();
    }

    void Update()
    {
        if (CirclePlayer != null)
        {
            Vector3 direction = CirclePlayer.transform.position - transform.position;
            direction.Normalize();
            transform.up = direction;
            if (canMove)
            {
                Move(direction);
            }
        }
        CheckPlayer();
    }
    void CheckPlayer()
    {
        int layerMask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(RaycastCheck.transform.position, RaycastCheck.transform.up, 0.6f, layerMask);

        if (hit.collider != null)
        {
            canMove = false;
            enemyAttackController.GetComponent<EnemyAttackController>()?.TriggerAttack(true); 
        }
        else
        {
            enemyAttackController.GetComponent<EnemyAttackController>()?.TriggerAttack(false);
            canMove = true;
        }
    }
    private void OnDrawGizmos()
    {
        if (RaycastCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                RaycastCheck.transform.position,
                RaycastCheck.transform.position + RaycastCheck.transform.up * 0.6f
            );
        }
    }

}