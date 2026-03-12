
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class AudioSelector : UdonSharpBehaviour
{
    //再生したい楽曲を格納する配列
    [Header("ここに再生したい曲をセット")]
    public AudioClip[] clips;
    //再生先
    [Header("再生先のAudioSourceを指定")]
    public AudioSource targetSource;
    //再生停止用無音スロット有効化チェックボックス
    [Header("再生オフ用チェックボックス")]
    public bool includeStopSlot = false;
    //現在再生してるスロット管理用変数
    [UdonSynced] private int currentIndex =0;

    private void ApplyCurrentState()
    {
        if(targetSource == null)return;

        if (includeStopSlot)
        {
            if(currentIndex == 0)
            {
                targetSource.Stop();
                targetSource.clip = null;
                return;
            }
            int clipIndex = currentIndex -1;
            if(clipIndex >= 0 && clipIndex < clips.Length && clips[clipIndex] != null)
            {
                targetSource.Stop();
                targetSource.clip = clips[clipIndex];
                targetSource.Play();
            }
        }
        else
        {
            if(clips == null || clips.Length == 0)return;
            if(currentIndex < 0 || currentIndex >= clips.Length)currentIndex =0;
            if(clips[currentIndex]==null)return;

            targetSource.Stop();
            targetSource.clip = clips[currentIndex];
            targetSource.Play();
        }
    }//ApplyCurrentState

    //いわゆるスイッチとしての処理
    public override void Interact()
    {
        int totalCount = clips != null ? clips.Length : 0;
        if(includeStopSlot) totalCount++;

        if(totalCount <= 0 || targetSource == null)return;

        VRCPlayerApi localPlayer = Networking.LocalPlayer;
        if (localPlayer != null && !Networking.IsOwner(gameObject))
        {
            Networking.SetOwner(localPlayer, gameObject);
        }

        currentIndex++;
        if(currentIndex >= totalCount)
        {
            currentIndex =0;
        }
        ApplyCurrentState();
        RequestSerialization();
    }

    //同期用処理
    public override void OnDeserialization()
    {
        ApplyCurrentState();
    }

    //いわゆるmain
    private void Start()
    {
        ApplyCurrentState();
    }
}
