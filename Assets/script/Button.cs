using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    //���ξ� �ҷ����� �޼ҵ�
    public void Start_B()
    {
        SceneManager.LoadScene("Main");
    }

    //����Ƽ �����͸� play ����, application�̸� �� ����
    public void End_B()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
