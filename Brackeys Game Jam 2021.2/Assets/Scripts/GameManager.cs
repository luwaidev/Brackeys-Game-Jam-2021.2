using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controls all entering and exiting scenes, as well as saving high score

public class GameManager : MonoBehaviour
{
    [Header("References")]
    private GameObject mc;
    public GameObject pauseObject;
    public Canvas canvas;
    public float sceneTransitionTime;

    public bool sceneLoading;
    public bool paused;

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
    }

    public void Play(){
        if (!sceneLoading) StartCoroutine(LoadNewScene("Game"));
    }

    public IEnumerator LoadNewScene(string sceneName){
        sceneLoading = true;

        mc = null;
        yield return new WaitForSeconds(sceneTransitionTime);
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!load.isDone){
            yield return null;
        }

        yield return new WaitForEndOfFrame();
        
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
}
