
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CycleSwitch : UdonSharpBehaviour
{
    [Header("切り替え対象")]
    public GameObject[] targets;
    //操作対象Object用配列　1次元配列

    [Header("初期表示オブジェクト")]
    public int startIndex = 0;
    //状態同期用
    [UdonSynced]
    private int currentIndex = 0;


    void Start()
    {
        currentIndex = startIndex;
        UpdateObjects();
    }
    public override void Interact()
    {
        if(targets == null || targets.Length ==0) return;//制御対象が無ければ実行しない

        Networking.SetOwner(Networking.LocalPlayer,gameObject);

        currentIndex++;
        if(currentIndex >= targets.Length)
        {
            currentIndex =0;
        }
        UpdateObjects();
        RequestSerialization();
    }

    public override void OnDeserialization()
    {
        UpdateObjects();
    }
    private void UpdateObjects()
    {
    for(int i = 0; i < targets.Length; i++)
        {
            if(targets[i] != null)
            {
                targets[i].SetActive(i == currentIndex);
            }
        }
    }
}
