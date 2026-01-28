using Assets.HeroEditor.Common.CommonScripts;
using UnityEngine;

public class GreedyStart : TeamBehaviour
{
    [SerializeField] private SpriteRenderer backgroundRenderer;
    protected override void ResetValue()
    {
        base.ResetValue();
        backgroundRenderer = GetComponent<SpriteRenderer>();
        backgroundRenderer.enabled = false;
    }

    private void Start()
    {
        backgroundRenderer.enabled = true;
    }
}
