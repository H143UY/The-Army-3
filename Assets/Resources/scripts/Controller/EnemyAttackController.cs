using System.Collections;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private Animator anim;
    public bool attack;
    public float attspeed;
    private void Start()
    {
        anim = GetComponent<Animator>();
        attack = false;
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
        anim.SetBool("attack", attack);
    }
    public void PauseAnimator()
    {
        StartCoroutine(PauseForSeconds(attspeed));
    }

    private IEnumerator PauseForSeconds(float time)
    {
        anim.speed = 0;
        yield return new WaitForSeconds(time);
        anim.speed = 1;
    }
}
