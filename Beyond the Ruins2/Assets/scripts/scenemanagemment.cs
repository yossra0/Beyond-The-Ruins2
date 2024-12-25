using UnityEngine;
using UnityEngine.SceneManagement; 
public class SceneLoader : MonoBehaviour
{
    public void LoadYourScene()
    {
        SceneManager.LoadScene("start"); 
    }
}
