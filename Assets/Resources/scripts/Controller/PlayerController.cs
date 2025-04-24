using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Core.Pool;
using PolyNav;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private GameObject currentTarget;
    public GameObject RaycastCheck;
    public Text textHp;
    public int HpPlayer;
    private WeponPlayerController weponPlayerControllerl;
    public float AttackSpeed;
    public int PowerAtt;

    private PolyNavAgent agent;
    private PatrolRandomWaypoints patrol;
    private bool isCounterAttacking = false;
    private Coroutine attackCoroutine;

    private void Awake()
    {
        weponPlayerControllerl = GetComponentInChildren<WeponPlayerController>();
        weponPlayerControllerl.playerController = this;

        if (instance == null)
        {
            instance = this;
        }

        agent = GetComponent<PolyNavAgent>();
        patrol = GetComponent<PatrolRandomWaypoints>();
    }

    void Update()
    {
        if (!isCounterAttacking)
        {
            CheckAttack();
        }

        textHp.text = HpPlayer.ToString();

        if (HpPlayer <= 0)
        {
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
            }
            SmartPool.Instance.Despawn(this.gameObject);
        }
    }

    void CheckAttack()
    {
        int layerMask = LayerMask.GetMask("Enemy", "Square");
        RaycastHit2D hit = Physics2D.Raycast(RaycastCheck.transform.position, RaycastCheck.transform.up, 0.6f, layerMask);

        if (hit.collider != null)
        {
            currentTarget = hit.collider.gameObject;
            weponPlayerControllerl.TriggerAttack(true);
        }
        else
        {
            currentTarget = null;
            weponPlayerControllerl.TriggerAttack(false);
        }
    }

    public void DoDamage()
    {
        if (currentTarget != null)
        {
            TryDamageTarget(currentTarget);
        }
    }

    void TryDamageTarget(GameObject target)
    {
        if (target.TryGetComponent(out EnemyController enemy))
        {
            enemy.TakeDamage(PowerAtt);
        }
        else if (target.TryGetComponent(out TileData tile))
        {
            tile.TakeDamage(PowerAtt);
        }
    }

    public void TakeDamage(int damage, GameObject attacker)
    {
        HpPlayer -= damage;
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        if (attacker != null)
        {
            attackCoroutine = StartCoroutine(AttackUntilEnemyDies(attacker));
        }
    }
    IEnumerator AttackUntilEnemyDies(GameObject enemy)
    {
        var nav = GetComponent<PolyNavAgent>();
        if (nav != null && enemy != null)
        {
            nav.SetDestination(enemy.transform.position);
        }

        while (enemy != null && enemy.activeInHierarchy)
        {
            currentTarget = enemy;
            weponPlayerControllerl.TriggerAttack(true);

            yield return new WaitForSeconds(AttackSpeed);

            if (enemy.TryGetComponent(out EnemyController ec))
            {
                if (ec != null && ec.gameObject.activeInHierarchy == false)
                {
                    break;
                }
            }
            else
            {
                break;
            }
        }
        currentTarget = null;
        weponPlayerControllerl.TriggerAttack(false);
        GetComponent<PatrolRandomWaypoints>()?.MoveRandom();
    }

    IEnumerator CounterAttack(GameObject enemy)
    {
        isCounterAttacking = true;
        agent.Stop();

        agent.SetDestination(enemy.transform.position);

        while (Vector2.Distance(transform.position, enemy.transform.position) > 0.6f)
        {
            yield return null;
        }

        currentTarget = enemy;
        weponPlayerControllerl.TriggerAttack(true);

        yield return new WaitForSeconds(2f);

        currentTarget = null;
        weponPlayerControllerl.TriggerAttack(false);

        isCounterAttacking = false;

        patrol.MoveRandomExternally(); // quay lại di chuyển tile
    }

    public void SetHP(float value)
    {
        HpPlayer = Mathf.RoundToInt(value);
    }

    public void SetAttackSpeed(float value)
    {
        AttackSpeed = value;
    }

    public void SetPower(float value)
    {
        PowerAtt = Mathf.RoundToInt(value);
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

