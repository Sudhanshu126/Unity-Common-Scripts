using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    //Public fields
    public GameLevel levelToLoad;

    //Properties
    public static LoadingScreen Instance { get; private set; }

    //Exposed private fields
    [Header("References")]
    [SerializeField] private Animator screenAnimator;
    [SerializeField] private RectTransform screenMask, background, screenFitCanvas;

    //---MonoBehaviour methods---
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetScreenSize();
    }

    //---Class methods---
    //Starts loading new scene
    public void StartSceneLoad(int buildIndex)
    {
        StartCoroutine(LoadScene(buildIndex));
    }

    //Loads new scene
    private IEnumerator LoadScene(int buildIndex)
    {
        screenAnimator.SetTrigger("Open");

        yield return new WaitForSeconds(2f);

        //SceneManager.LoadScene(buildIndex);
        
        AsyncOperation sceneOperation = SceneManager.LoadSceneAsync(buildIndex);
        sceneOperation.allowSceneActivation = false;

        while(sceneOperation.progress < 0.9f)
        {
            //Can show scene load progress
            yield return null;
        }

        sceneOperation.allowSceneActivation = true;
        screenAnimator.SetTrigger("Close");
    }

    //---UI methods---
    private void SetScreenSize()
    {
        Vector2 canvasHeight = screenFitCanvas.sizeDelta;
        background.sizeDelta = canvasHeight;
    }
}w
