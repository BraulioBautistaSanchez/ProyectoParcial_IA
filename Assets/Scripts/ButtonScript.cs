using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonScript : MonoBehaviour
{
    public void GoToScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}