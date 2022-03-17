using UnityEngine.SceneManagement;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    
    void Start()
    {
        Invoke("ToSampleScene", 2f);
    }

    private void ToSampleScene()
    {
        SceneManager.LoadScene("01_Sample Scene");
    }
}
