using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Target;
    public int Damage;
    public float Height;
    private Vector3 start;
    private Vector3 up;
    private Vector3 end;
    private Vector3 targetUp;
    private float value = 0f;

    private void Start()
    {
        start = transform.position;
        up = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        end = new Vector3(Target.transform.position.x, 0f, Target.transform.position.z);
        targetUp = new Vector3(Target.transform.position.x, Target.transform.position.y + 3f, Target.transform.position.z);
    }

    private void Update()
    {
        value += Time.deltaTime;

        end = new Vector3(Target.transform.position.x, 0f, Target.transform.position.z);

        SetBulletPos(start, up, targetUp, end, value);
    }

    void SetBulletPos(Vector3 player, Vector3 playerUp, Vector3 targetUp, Vector3 target, float value)
    {
        Vector3 A = Vector3.Lerp(player, playerUp, value);
        Vector3 B = Vector3.Lerp(playerUp, targetUp, value);
        Vector3 C = Vector3.Lerp(targetUp, target, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        Vector3 E = Vector3.Lerp(B, C, value);

        Vector3 F = Vector3.Lerp(D, E, value);

        transform.position = F;
    }

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