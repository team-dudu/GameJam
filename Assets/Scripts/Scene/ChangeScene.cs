using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    public int targetScene = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (targetScene != -1)
            {
                SceneManager.LoadScene(targetScene);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}