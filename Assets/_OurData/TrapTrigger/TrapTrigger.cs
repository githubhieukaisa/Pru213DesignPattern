using UnityEngine;

public class TrapTrigger : TeamBehaviour
{
    [Header("References")]
    [SerializeField] private Transform trap;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform nextSceneBtn;
    protected override void LoadComponents()
    {
        if (trap == null)
        {
            trap = GameObject.Find("Grid").transform.Find("TrapTrigger").transform;
            trap.gameObject.SetActive(false);
        }

        if (nextSceneBtn == null)
        {
            nextSceneBtn = GameObject.Find("BtnNextScene")?.transform;
        }
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        playerLayer = LayerMask.GetMask("Player");
    }

    void Start()
    {
        trap.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            trap.gameObject.SetActive(true);
            if (nextSceneBtn != null)
            {
                nextSceneBtn.gameObject.SetActive(false);
            }
        }
    }
}
