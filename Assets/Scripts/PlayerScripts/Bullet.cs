using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public AudioClip boxHit,plankHit,groundHit,explodeHit;
    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.CompareTag("Box"))
        {
            SoundManager.instance.PlaySoundFX(boxHit,1f);
            Destroy(target.gameObject);
        }
        else if(target.gameObject.CompareTag("Ground"))
        {
            SoundManager.instance.PlaySoundFX(groundHit,1f);
        }
        else if(target.gameObject.CompareTag("Plank"))
        {
            SoundManager.instance.PlaySoundFX(plankHit,1f);
        }
        else if(target.gameObject.CompareTag("Tnt"))
        {
            SoundManager.instance.PlaySoundFX(explodeHit,1f);
        }
    }
}
