using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed=100,bulletSpeed=100;
    private Transform handPos,firePos1,firePos2;
    private LineRenderer lineRenderer;
    public GameObject bullet;
    public int bulletAmmo=4;
    private bool isStart=false;
    public GameObject crossHair;
    public AudioClip gunShoot;
    // Start is called before the first frame update
    void Awake()
    {
        crossHair.SetActive(false);
        handPos=GameObject.FindGameObjectWithTag("LeftArm").transform;
        firePos1=GameObject.FindGameObjectWithTag("FirePos1").transform;
        firePos2=GameObject.FindGameObjectWithTag("FirePos2").transform;
        lineRenderer=GameObject.FindGameObjectWithTag("GunPos").GetComponent<LineRenderer>();
        lineRenderer.enabled=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMouseOverUI() /*&& !isStart*/)
        {
            if(Input.GetMouseButton(0))
            {
            Aim();
            //isStart=true;
            }
            if(Input.GetMouseButtonUp(0))
            {
                if(bulletAmmo>0)
                {
                    Shoot();    
                }
                else
                {
                lineRenderer.enabled=false;
                crossHair.SetActive(false);
                //crosshair
                }
                //isStart=false;
            }
        }
    }
    void Aim()
    {
        Vector2 direction=Camera.main.ScreenToWorldPoint(Input.mousePosition)-handPos.position;
        float angle=Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg+90;
        Quaternion rotation=Quaternion.AngleAxis(angle,Vector3.forward);
        handPos.rotation=Quaternion.Slerp(transform.rotation,rotation,rotateSpeed*Time.deltaTime);
        lineRenderer.enabled=true;
        lineRenderer.SetPosition(0,firePos1.position);
        lineRenderer.SetPosition(1,firePos2.position);
        crossHair.SetActive(true);
        crossHair.transform.position=Camera.main.ScreenToWorldPoint(Input.mousePosition+(Vector3.forward*10));
    }
    void Shoot()
    {   
        crossHair.SetActive(false);
        lineRenderer.enabled=false;
        GameObject b=Instantiate(bullet,firePos1.position,Quaternion.identity);
        if(transform.localScale.x>0)
        {
            b.GetComponent<Rigidbody2D>().AddForce(firePos1.right*bulletSpeed,ForceMode2D.Impulse);
        }
        else
        {
            b.GetComponent<Rigidbody2D>().AddForce(-firePos1.right*bulletSpeed,ForceMode2D.Impulse);
        }
        bulletAmmo--;
        FindObjectOfType<GameManager>().CheckBullet();
        SoundManager.instance.PlaySoundFX(gunShoot,0.3f);
        
        Destroy(b,1.5f);

    }
    bool isMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
