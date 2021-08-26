using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Controls all entering and exiting scenes, as well as saving high score

public class GameManager : MonoBehaviour
{
    [Header("References")]
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(){
        if (!sceneLoading) StartCoroutine(LoadNewScene("Play"));
    }

    public IEnumerator LoadNewScene(string sceneName){
        sceneLoading = true;
        yield return new WaitForSeconds(sceneTransitionTime);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        sceneLoading = false;
    }

    void Pause(){
        if (Input.GetKeyDown(KeyCode.Escape)) paused = !paused;

        if (paused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
