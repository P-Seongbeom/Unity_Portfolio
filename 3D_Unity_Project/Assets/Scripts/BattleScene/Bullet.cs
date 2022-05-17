using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;
    public float Height;
    public string myTag;

    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.tag == "Ground")
        //{
        //    //    print("������");
        //    Destroy(gameObject);
        //}
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != myTag)
        {
            if(other.gameObject.tag == "Enemy")
            {
                //print("������");
                other.gameObject.GetComponent<EnemyBattleController>().Damaged(Damage);
                Destroy(gameObject);
            }
            else if(other.gameObject.tag == "MyPet")
            {
                //print("�Ʊ�����");
                other.gameObject.GetComponent<PlayerPetBattleController>().Damaged(Damage);
                Destroy(gameObject);
            }
            else if (other.gameObject.tag == "Ground")
            {
                //print("������");
                Destroy(gameObject);
            }
        }

    }

}