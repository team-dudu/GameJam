using GameJam;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetComponentInParent<Animator>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.otherCollider.isTrigger = true;
            _animator.SetAnimation(AnimationParameter.Open);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}