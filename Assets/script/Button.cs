using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    //메인씬 불러오는 메소드
    public void Start_B()
    {
        SceneManager.LoadScene("Main");
    }

    //유니티 에디터면 play 끄고, application이면 앱 종료
    public void End_B()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
