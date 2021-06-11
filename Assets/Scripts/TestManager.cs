using UnityEngine;
using yaSingleton;

[CreateAssetMenu(fileName = "TestManager", menuName = "Singletons/TestManager")]
public class TestManager : Singleton<TestManager>
{
    public void HelloSingleton()
    {
        Debug.Log("Hello singleton");
    }
}
