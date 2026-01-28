using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPointScript : TeamBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private string nextSceneName = "Command";
    protected override void ResetValue()
    {
        base.ResetValue();
        playerLayer = LayerMask.GetMask("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            NextScene(nextSceneName);
        }
    }

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
