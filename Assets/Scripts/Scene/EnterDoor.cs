using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterDoor : MonoBehaviour
{
    private int targetScene = -1;
    // Start is called before the first frame update
    void Start()
    {
        targetScene = transform.parent.GetComponent<ChangeScene>().targetScene;
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
