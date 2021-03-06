using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Controls all entering and exiting scenes, as well as saving high score

public class GameManager : MonoBehaviour
{
    [Header("References")]
    private GameObject mc;
    public GameObject pauseObject;
    public Canvas canvas;
    public Animator fade;
    public float sceneTransitionTime;

    public bool sceneLoading;
    public bool paused;

    [Header("In Game references")]
    private LevelManager lm;
    public int bestScore;
    public int previousScore;

    private void Awake() {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1) Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        mc = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (mc != null) transform.position = mc.transform.position;

        Pause();
        InGame();
    }

    public void Play(){
        if (!sceneLoading) StartCoroutine(LoadNewScene("Game"));
    }
    public void ReturnToMenu()
    {
        if (!sceneLoading) StartCoroutine(LoadNewScene("Menu"));
    }

    public IEnumerator LoadNewScene(string sceneName){
        sceneLoading = true;

        mc = null;

        fade.SetBool("Fade", true);
        yield return new WaitForSeconds(sceneTransitionTime);
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!load.isDone){
            yield return null;
        }

        
        fade.SetBool("Fade", false);

        yield return new WaitForEndOfFrame();
        
        if (SceneManager.GetActiveScene().name == "Game")
        {
            lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        }   
        else if (SceneManager.GetActiveScene().name == "Menu")
        {
            GameObject.Find("BestScore").GetComponent<TMP_Text>().text = bestScore.ToString();
            GameObject.Find("PreviousScore").GetComponent<TMP_Text>().text = previousScore.ToString();
        }   
        else if (SceneManager.GetActiveScene().name == "End")
        {
            GameObject.Find("PreviousScore").GetComponent<TMP_Text>().text = previousScore.ToString();
        }
        mc = Camera.main.gameObject;
        sceneLoading = false;
    }

    void Pause(){
        if (Input.GetKeyDown(KeyCode.Escape)) paused = !paused;

        if (paused)
        {
            Time.timeScale = 0; 
            pauseObject.SetActive(true);
        }
         
        else 
        {
            Time.timeScale = 1;
            pauseObject.SetActive(false);
        }
    }

    void InGame()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (lm.deliveries > bestScore) bestScore = lm.deliveries;
            previousScore = lm.deliveries;
            print(lm.failures);
            if (!sceneLoading && lm.failures >= 3) LoadNewScene("End");
        }
    }

    

}
