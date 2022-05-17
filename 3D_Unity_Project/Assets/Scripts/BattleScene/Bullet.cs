using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Target;
    public int Damage;
    public float Height;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Target)
        {
            Target.GetComponent<BattleController>().Damaged(Damage);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

}