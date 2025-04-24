using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponPlayerController : MonoBehaviour
{
    public Animator _animator;
    public bool Attack;
    public PlayerController playerController;
    void Start()
    {
        _animator = GetComponent<Animator>();
        Attack = true;
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
        StartCoroutine(PauseForSeconds(PlayerController.instance.AttackSpeed));
    }

    private IEnumerator PauseForSeconds(float time)
    {
        _animator.speed = 0;
        yield return new WaitForSeconds(time);
        _animator.speed = 1;
    }
    public void DoDamage()
    {
        if (playerController != null)
        {
            playerController.DoDamage(); 
        }
    }
}
