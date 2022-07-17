using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : MonoBehaviour
{
    public Projectile fireball;
    public float playerDistance = 10f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Player.Instance.transform.position - transform.position).magnitude < playerDistance)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Projectile projectile = Instantiate(fireball, transform.position, transform.rotation);
        yield return new WaitForSeconds(3f);
    }
}
