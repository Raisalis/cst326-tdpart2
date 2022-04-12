using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform targetpos;
    public MobaEnemy targetEnemy;
    public float speed = 100f;
    public int power;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(targetpos == null) {
            Destroy(this.gameObject);
            return;
        }

        Vector3 direction = targetpos.position - transform.position;
        float dist = speed * Time.deltaTime;

        if(direction.magnitude <= dist) {
            Hit();
            return;
        }

        transform.Translate(direction.normalized * dist, Space.World);
    }

    public void Fire(Transform incomingpos, MobaEnemy incomingEnemy, int incomingpower) {
        targetpos = incomingpos;
        power = incomingpower;
        targetEnemy = incomingEnemy;
    }

    void Hit() {
        if(targetEnemy != null) {
            targetEnemy.hit(power);
        }
        Destroy(this.gameObject);
    }
}
