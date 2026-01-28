using UnityEngine;
using UnityEngine.SceneManagement;

public class Deadzone : TeamBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private string scene = "Greedy";

    protected override void ResetValue()
    {
        base.ResetValue();
        playerLayer = LayerMask.GetMask("Player");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene(scene);
        }
    }
}
