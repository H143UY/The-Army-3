using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject CirclePlayer;
    public GameObject RaycastCheck;
    //public Text textHp;
    private int HpPlayer;
    private WeponPlayerController weponPlayerControllerl;
    private void Awake()
    {
        weponPlayerControllerl = GetComponentInChildren<WeponPlayerController>();
    }
    void Start()
    {
        HpPlayer = 3;
    }

    void Update()
    {
        CheckAttack();
    }
    void CheckAttack()
    {
        int layerMask = LayerMask.GetMask("Enemy", "Square");
        RaycastHit2D hit = Physics2D.Raycast(RaycastCheck.transform.position, RaycastCheck.transform.up, 0.6f, layerMask);
        if (hit.collider != null)
        {
            weponPlayerControllerl.TriggerAttack(true);
        }
        else
        {
            weponPlayerControllerl.TriggerAttack(false);
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
