using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private AsyncOperation loadingOperation;
    
   
    private void Update()
    {
        if (CharacterHealth.isDead && Input.GetKeyDown(KeyCode.Space)) 
        {
            
            StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
        }
    }


    private IEnumerator LoadScene(string levelName) 
    {
        
        yield return null;
        loadingOperation = SceneManager.LoadSceneAsync(levelName);
        
        
        while (!loadingOperation.isDone) 
        {
            
            yield return null;
        }
        
        

    }
}
