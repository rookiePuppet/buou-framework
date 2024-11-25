using UnityEngine;

namespace BuouFramework.UI
{
    public interface IUIManagerInitializer
    {
        GameObject ProvideCanvasObject();
        GameObject ProvideCameraObject();
        GameObject ProvideEventSystemObject();
    }
}