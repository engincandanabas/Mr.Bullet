using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameUI instance;
    private GameManager gameManager;
    private int startBB;

    [Header("Win Screen")]
    public Text goodJobText;
    public GameObject winPanel;
    public Image star1,star2,start3;
    public Sprite shineStar,darkStart;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    void Awake()
    {
        instance=this;
        gameManager=FindObjectOfType<GameManager>();
        
    }
    void Start()
    {
        startBB=gameManager.blackBullet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
    public void WinScreen()
    {
        winPanel.SetActive(true);
        if(gameManager.blackBullet>=startBB)
        {
            goodJobText.text=" FANTASTIC!";
            StartCoroutine(Stars(3));
            
        }
        else if(gameManager.blackBullet>=startBB-(gameManager.blackBullet/2))
        {
            goodJobText.text=" AWESOME!";
            StartCoroutine(Stars(2));

        }
        else if(gameManager.blackBullet>0)
        {
            goodJobText.text=" WELL DONE!";
            StartCoroutine(Stars(1));
        }
        else
        {
            goodJobText.text=" GOOD";
            StartCoroutine(Stars(0));
        }
    }
    private IEnumerator Stars(int shineNumber)
    {
        yield return new WaitForSeconds(0.5f);
        switch(shineNumber)
        {
            case 3:
                yield return new WaitForSeconds(0.15f);
                star1.sprite=shineStar;
                yield return new WaitForSeconds(0.15f);
                star2.sprite=shineStar;
                yield return new WaitForSeconds(0.15f);
                start3.sprite=shineStar;
                break;
            case 2:
                yield return new WaitForSeconds(0.15f);
                star1.sprite=shineStar;
                yield return new WaitForSeconds(0.15f);
                star2.sprite=shineStar;
                start3.sprite=darkStart;
                break;
            case 1:
                yield return new WaitForSeconds(0.15f);
                star1.sprite=shineStar;
                star2.sprite=darkStart;
                start3.sprite=darkStart;
                break;
            case 0:
                star1.sprite=darkStart;
                star2.sprite=darkStart;
                start3.sprite=darkStart;
                break;

        }
    }
}
