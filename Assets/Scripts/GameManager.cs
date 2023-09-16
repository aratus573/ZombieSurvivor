using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Text = TMPro.TextMeshProUGUI;

public class GameManager : MonoBehaviour
{
    // 現在按esc可以暫停遊戲，再按一次繼續
    public int[] clock = new int[] {0,0,0,0}; // 計時結果，[0][1]分，[2][3]秒
    public static bool gameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject GameOverUI,GameWinUI,PauseUI;
    public UpgradesManager upgradesManager;
    public bool gameHasEnded = false;

    [SerializeField] Text timerText;

    public KeyCode pauseKey;
    public KeyCode restartKey;


    private float time = 0; // 用+=Time.deltaTime做計時
    [SerializeField] float survivedTime=0;
    public float restartDelay = 5f;
    
    void Awake()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        InputManager();
        Timer();
        survivedTime+=Time.deltaTime;
        if(survivedTime>=600){
            WinGame();
        }
    }
    
    void InputManager()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (gameIsPaused)
            {
                PauseUI.SetActive(false);
                Resume();
            }
            else
            {
                PauseUI.SetActive(true);
                Pause();
            }
        }

        if (Input.GetKeyDown(restartKey))
        {
            Restart();
        }
        // if (Input.GetKeyDown(KeyCode.P))
        // {upgradesManager.LevelUp();}
    }
    

    void Timer()
    {
        time += Time.deltaTime;
        if (time >= 1f)
        {
            clock[3] ++ ;
            time = 0f;
        }
        if (clock[3] > 9) // 00:09 -> 00:10
        {
            clock[3] = 0;
            clock[2] ++ ;
        }
        if (clock[2] > 5) // 00:59 -> 01:00
        {
            clock[2] = 0;
            clock[1] ++ ;
        }
        if (clock[1] > 9) // 09:59 -> 10:00
        {
            clock[1] = 0;
            clock[0] ++ ;
        }
        // 正式遊玩應該用不到
        if (clock[0] > 5) //59:59 -> 00:00
        {
            clock[0] = 0;
        }
        timerText.text=clock[0]+clock[1]+":"+clock[2]+clock[3];
    }

    public void Pause()
    {
        // 如果有的話再取消註解
        // PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        // 如果有的話再取消註解
        // PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void LoadMenu()
    {
        // 如果有的話再取消註解
        SceneManager.LoadScene("Menu");
        Debug.Log("loading menu");
    }

    public void EndingGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            // 結束遊戲
            // GameOverUI.SetActive(true);
            Pause();
            GameOverUI.SetActive(true);
            //Invoke(nameof(Restart),restartDelay); // 等待後重新開始，待討論
        }
    }
    public void WinGame(){
        gameHasEnded = true;
        GameWinUI.SetActive(true);
        Pause();
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.Instance.PlayMusic("Theme");
    }


}
