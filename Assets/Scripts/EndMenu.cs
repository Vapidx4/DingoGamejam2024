using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
        {
            Debug.Log("KeyDown");
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene(1);
            }
        }

    }
}
