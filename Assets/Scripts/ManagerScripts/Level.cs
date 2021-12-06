using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private Button levelBTN;
    public int levelReq;
    // Start is called before the first frame update
    void Start()
    {
        levelBTN=GetComponent<Button>();
        if(PlayerPrefs.GetInt("level",1)>=levelReq)
        {
            levelBTN.onClick.AddListener(()=>LoadLevel());
        }
        else{
            GetComponent<CanvasGroup>().alpha=0.5f;
        }
    }

    // Update is called once per frame
    public void LoadLevel()
    {
        SceneManager.LoadScene(gameObject.name);
    }
}
