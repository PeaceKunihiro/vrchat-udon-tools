using UdonSharp;
using UnityEngine;

public class AutoHideObject : UdonSharpBehaviour
{
    [Header("有効化後に非表示にするまでの秒数")]
    public float lifeTime = 5f;

    private float disableAt;

    private void OnEnable()
    {
        disableAt = Time.time + lifeTime;
        SendCustomEventDelayedSeconds(nameof(TryDisable), lifeTime);
    }

    public void TryDisable()
    {
        float remain = disableAt - Time.time;

        if (remain > 0.01f)
        {
            SendCustomEventDelayedSeconds(nameof(TryDisable), remain);
            return;
        }

        gameObject.SetActive(false);
    }
}