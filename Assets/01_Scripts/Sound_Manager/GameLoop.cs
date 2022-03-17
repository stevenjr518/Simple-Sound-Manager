using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour
{
    private Button button_playBG01;
    private Button button_playBG02;
    private Button button_playS01;
    private Button button_playS02;

    private void Start()
    {
        button_playBG01 = GameObject.Find("Play_BG01").GetComponent<Button>();
        button_playBG02 = GameObject.Find("Play_BG02").GetComponent<Button>();
        button_playS01 = GameObject.Find("Play_Sound01").GetComponent<Button>();
        button_playS02 = GameObject.Find("Play_Sound02").GetComponent<Button>();

        if (Sound_Manager.instance != null)
        {
            button_playBG01.onClick.AddListener(() => Sound_Manager.instance.OnPlayClip(0));
            button_playBG02.onClick.AddListener(() => Sound_Manager.instance.OnPlayClip(1));
            button_playS01.onClick.AddListener(() => Sound_Manager.instance.OnPlayOneShot(2));
            button_playS02.onClick.AddListener(() => Sound_Manager.instance.OnPlayOneShot(3));
        }
    }
}
