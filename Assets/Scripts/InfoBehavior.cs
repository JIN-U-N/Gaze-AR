using UnityEngine;
using System.Linq;


public class InfoBehavior : MonoBehaviour
{
    const float Speed=6f;

    [SerializeField]
    Transform SectionInfo;

    Vector3 desiredScale= Vector3.zero;

    void Update()
    {
        SectionInfo.localScale=Vector3.Lerp(SectionInfo.localScale,desiredScale,Time.deltaTime*Speed);
    }

    public void OpenInfo()
    {
        desiredScale=Vector3.one;
    }

    public void CloseInfo()
    {
        desiredScale=Vector3.zero;
    }
}
