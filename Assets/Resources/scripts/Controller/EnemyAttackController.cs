using System.Collections;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private Animator anim;
    public bool attack;
    public float attspeed;
    private bool CanAttack;
    public EnemyController EnemyController;
    private void Start()
    {
        anim = GetComponent<Animator>();
        attack = false;
        CanAttack = true;
    }
    private void Update()
    {
        ChangeAnim();
    }
    public void TriggerAttack(bool value)
    {
        attack = value;
    }
    private void ChangeAnim()
    {
        if (CanAttack)
        {
            anim.SetBool("attack", attack);
        }
    }
    public void PauseAnimator()
    {
        StartCoroutine(PauseForSeconds(attspeed));
    }

    private IEnumerator PauseForSeconds(float time)
    {
        CanAttack = false;
        attack = false;
        anim.speed = 0;
        yield return new WaitForSeconds(time);
        anim.speed = 1;
        CanAttack = true;
    }
    public void DoDamage()
    {
        if (EnemyController != null)
        {
            EnemyController.DoDamage();
        }
    }
}
