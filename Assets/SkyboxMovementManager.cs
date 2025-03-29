using UnityEngine;

public class SkyboxMovementManager : MonoBehaviour
{

    public float skyBoxSpeed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyBoxSpeed);
    }
}
