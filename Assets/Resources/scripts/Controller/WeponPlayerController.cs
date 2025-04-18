using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponPlayerController : MonoBehaviour
{
    private Animator _animator;
    public bool Attack;
    private float AttSpeed;
    void Start()
    {
        _animator = GetComponent<Animator>();
        Attack = true;
        AttSpeed = 1f;
    }

    void Update()
    {
        ChangeAnim();
    }
    public void TriggerAttack(bool value)
    {
        Attack = value;
    }
    private void ChangeAnim()
    {
        _animator.SetBool("att", Attack);
    }
    public void PauseAnimator()
    {
        StartCoroutine(PauseForSeconds(AttSpeed));
    }

    private IEnumerator PauseForSeconds(float time)
    {
        _animator.speed = 0;
        yield return new WaitForSeconds(time);
        _animator.speed = 1;
    }
}
