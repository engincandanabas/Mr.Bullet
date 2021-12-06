using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tnt : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float radius=1,power=10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Explode()
    {
        Vector2 explosionPos=transform.position;
        Collider2D[] colliders=Physics2D.OverlapCircleAll(explosionPos,radius);
        foreach(Collider2D hit in colliders)
        {
            if(hit.GetComponent<Rigidbody2D>() !=null)
            {
                var explodeDir=hit.GetComponent<Rigidbody2D>().position-explosionPos;
                hit.GetComponent<Rigidbody2D>().gravityScale=1f;  
                hit.GetComponent<Rigidbody2D>().AddForce(power*explodeDir,ForceMode2D.Impulse);
            }
            if(hit.tag=="Enemy")
            {
                hit.tag="Untagged";
            }
        }
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.green;
        Gizmos.DrawSphere(transform.position,radius);
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.CompareTag("Bullet"))
        {
            GameObject exp=Instantiate(explosionPrefab);
            exp.transform.position=transform.position;
            Explode();
            Destroy(exp,0.8f);
            Destroy(gameObject);
        }
    }
}
