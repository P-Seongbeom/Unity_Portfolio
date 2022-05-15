using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public float Height;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            print("������");
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Enemy")
        {
            print("������");
            collision.gameObject.GetComponent<EnemyBattleController>().Damaged(Damage);
            Destroy(gameObject);
        }
    }


}
