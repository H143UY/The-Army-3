using Core.Pool;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : CircleController
{
    private GameObject CirclePlayer;
    public bool canMove;
    private GameObject currentTarget;
    public GameObject RaycastCheck;
    private EnemyAttackController enemyAttackController;
    public Text HpText;
    private int Hp;
    public float attackSpeed;

    void Start()
    {
        enemyAttackController = GetComponentInChildren<EnemyAttackController>();
        enemyAttackController.EnemyController = this;
    }

    void Update()
    {
        CirclePlayer = GameObject.FindWithTag("Player");
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
        if (Hp <= 0)
        {
            SmartPool.Instance.Despawn(this.gameObject);
        }
        CheckPlayer();
        HpText.text = Hp.ToString();
    }
    void CheckPlayer()
    {
        int layerMask = LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(RaycastCheck.transform.position, RaycastCheck.transform.up, 0.3f, layerMask);

        if (hit.collider != null)
        {
            currentTarget = hit.collider.gameObject;
            canMove = false;
            enemyAttackController.GetComponent<EnemyAttackController>()?.TriggerAttack(true);
        }
        else
        {
            //enemyAttackController.GetComponent<EnemyAttackController>()?.TriggerAttack(false);
            canMove = true;
        }
    }
    public void SetHp(int value)
    {
        Hp = value;
    }
    private void OnDrawGizmos()
    {
        if (RaycastCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(
                RaycastCheck.transform.position,
                RaycastCheck.transform.position + RaycastCheck.transform.up * 0.3f
            );
        }
    }
    public void TakeDamage(int Damage)
    {
        Hp -= Damage;
    }
    public void DoDamage()
    {
        if (currentTarget != null)
        {
            PlayerController player = currentTarget.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1,this.gameObject);
            }
        }
        else
        {
            Debug.Log("ko tim thjay");
        }
    }
}