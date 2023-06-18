using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneNavigationManager : MonoBehaviour
{
    public static SceneNavigationManager instance;

    [Header("Miscellaneous Screens")]
    public int homeMenu;
    public int creditsScreen;

    [Header("Level-Related Screens")]
    public int level1Scene1;
    public int level1Scene2;

    public int level2Scene1;

    public int level3Scene1;
    public int level3Scene2;
    
    [Header("Cutscene Screens")]
    public int cs1BefL1S1;
    public int cs2BefL1S1;
    public int cs1AftL1S2;
    public int cs2AftL1S2;

    public int csBefL2S1;
    public int csAftL2S2;

    public int cs1AftL3S2;
    public int cs2AftL3S2;
    public int cs3AftL3S2;
    public int cs4AftL3S2;
    public int cs5AftL3S2;
    public int cs6AftL3S2;
    public int cs7AftL3S2;

    void Awake() 
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GoToScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    public void GoToHomeMenu()
    {
        SceneManager.LoadScene(homeMenu);
    }
    public void GoToL1S1()
    {
        SceneManager.LoadScene(level1Scene1);
    }
    public void GoToL1S2()
    {
        SceneManager.LoadScene(level1Scene2);
    }
    public void GoToL2S1()
    {
        SceneManager.LoadScene(level2Scene1);
    }
    public void GoToL3S1()
    {
        SceneManager.LoadScene(level3Scene1);
    }
    public void GoToL3S2()
    {
        SceneManager.LoadScene(level3Scene2);
    }
    public void GoTocs1BefL1S1()
    {
        SceneManager.LoadScene(cs1BefL1S1);
    }
    public void GoTocs2BefL1S1()
    {
        //SceneManager.LoadScene(cs2BefL1S1);
    }
    public void GoTocs1AftL1S2()
    {
        SceneManager.LoadScene(cs1AftL1S2);
    }
    public void GoTocs2AftL1S2()
    {
        SceneManager.LoadScene(cs2AftL1S2);
    }
    public void GoTocsBefL2S1()
    {
        SceneManager.LoadScene(csBefL2S1);
    }
}
