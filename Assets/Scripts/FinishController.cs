using UnityEngine;

public class FinishController : MonoBehaviour
{
    public GameObject image;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            image.SetActive(true);
            Time.timeScale = 0f;
        }   
    }
}
