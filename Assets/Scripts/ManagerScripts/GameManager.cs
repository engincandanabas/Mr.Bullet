using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public int enemyCount;
    [HideInInspector]
    public bool gameOver;
    public int blackBullet=3;
    public int goldenBullet=1;
    public GameObject blackBulletPrefab,goldenBulletPrefab;
    public GameObject gameOverPanel;
    private int levelNumber;

    // Start is called before the first frame update
    void Start()
    {
        levelNumber=PlayerPrefs.GetInt("level",1);

        gameOverPanel.SetActive(false);

        FindObjectOfType<PlayerController>().bulletAmmo=blackBullet+goldenBullet;
        for(int i=0;i<blackBullet;i++)
        {
            GameObject bTemp=Instantiate(blackBulletPrefab);
            
            bTemp.transform.SetParent(GameObject.Find("Bullets").transform);
            bTemp.transform.localScale=new Vector3(0.5f,0.5f,0);
        }
        for(int i=0;i<goldenBullet;i++)
        {
            GameObject bTemp=Instantiate(goldenBulletPrefab);

            bTemp.transform.SetParent(GameObject.Find("Bullets").transform);
            bTemp.transform.localScale=new Vector3(0.5f,0.5f,0);
        }
        CheckEnemy();

    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver && FindObjectOfType<PlayerController>().bulletAmmo<=0 && enemyCount>0 && GameObject.FindGameObjectsWithTag("Bullet").Length<=0)
        {
            gameOver=true; 
            GameUI.instance.GameOverPanel();
            print("GameOver");
        }
    }
    public void CheckBullet()
    {
        if(goldenBullet>0)
        {
            goldenBullet--;
            GameObject.FindGameObjectWithTag("GoldenBullet").SetActive(false);
        }
        else if(blackBullet>0)
        {
            blackBullet--;
            GameObject.FindGameObjectWithTag("BlackBullet").SetActive(false);
        }
    }
    public void CheckEnemy()
    {
        enemyCount=GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(enemyCount<=0)
        {
            GameUI.instance.WinScreen();   
            if(levelNumber<=SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("level",levelNumber+1);
                print("Level: "+PlayerPrefs.GetInt("level"));
            }
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
