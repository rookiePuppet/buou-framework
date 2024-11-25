using UnityEngine;

namespace BuouFramework.UI
{
    public enum LoadingWay
    {
        Resources,
        Addressables
    }
    public class UIManagerConfig : ScriptableObject
    {
        [SerializeField] private LoadingWay prefabLoadingWay = LoadingWay.Addressables;

        // public GameObject ProvideCanvasObject(string path)
        // {
        //     
        // }
        //
        // public GameObject ProvideCameraObject(string path)
        // {
        //     
        // }
        //
        // public GameObject ProvideEventSystemObject(string path)
        // {
        //     
        // }
        //
        // public Awaitable<GameObject> ProvideViewObject(string path)
        // {
        //     
        // }
    }
}