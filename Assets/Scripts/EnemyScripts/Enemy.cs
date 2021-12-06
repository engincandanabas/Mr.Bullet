using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public AudioClip death;
    void Death()
    {
        gameObject.tag="Untagged";
        FindObjectOfType<GameManager>().CheckEnemy();
        SoundManager.instance.PlaySoundFX(death,0.75f);
        foreach(Transform obj in transform)
        {
            obj.GetComponent<Rigidbody2D>().gravityScale=1;
        }
        StartCoroutine(trigger());
    }
    IEnumerator trigger()
    {
        yield return new WaitForSeconds(.3f);
        foreach(Transform obj in transform)
        {
            obj.GetComponent<CapsuleCollider2D>().isTrigger=false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            Vector2 direction=transform.position-other.transform.position;
            if(transform.GetChild(0).GetComponent<Rigidbody2D>().gravityScale<1)
            {
                Death();
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2((direction.x>0 ? 1 : -1)*10,(direction.y>0 ? .3f:-.3f)),ForceMode2D.Impulse);
        }
        if(other.gameObject.CompareTag("Plank") || other.gameObject.CompareTag("BoxPlank"))
        {
            if(other.GetComponent<Rigidbody2D>().velocity.magnitude>1.5f)
            {
                Death();
            }
        }
        if(other.gameObject.CompareTag("Ground"))
        {
            if(GetComponent<Rigidbody2D>().velocity.magnitude>2)
            {
                Death();
            }
        }
    }
}
