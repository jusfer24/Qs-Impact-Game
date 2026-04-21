using UnityEngine;

public class LifeAndToxity : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
           if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    } 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
