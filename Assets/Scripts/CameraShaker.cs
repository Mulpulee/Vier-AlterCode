using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    private static CameraShaker m_instance;
    public static CameraShaker Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<CameraShaker>();
                if (m_instance == null)
                {
                    GameObject instance = new GameObject("CameraShaker");
                    m_instance = instance.AddComponent<CameraShaker>();
                    DontDestroyOnLoad(instance);
                }
            }
            return m_instance;
        }
    }

    private static CinemachineVirtualCamera m_vc;
    private static CinemachineVirtualCamera vc
    {
        get
        {
            if (m_vc == null)
            {
                m_vc = FindObjectOfType<CinemachineVirtualCamera>();
            }
            return m_vc;
        }
        set { m_vc = value; }
    }
    private static CinemachineImpulseSource m_source;
    private static CinemachineImpulseSource source
    {
        get
        {
            if (m_source == null)
            {
                m_source = FindObjectOfType<CinemachineImpulseSource>();
            }
            return m_source;
        }
        set { m_source = value; }
    }

    public void ShakeCamera(float pPower, float pTime = 0.3f)
    {
        StartCoroutine(ShakeCameraRoutine(pTime, pPower));
    }

    private IEnumerator ShakeCameraRoutine(float pTime, float pPower)
    {
        CinemachineBasicMultiChannelPerlin noise = vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = pPower;
        noise.m_FrequencyGain = 2;
        yield return new WaitForSeconds(pTime);
        noise.m_AmplitudeGain = 0;
    }

    public void ShakeCamImpulse(float pPower)
    {
        source.GenerateImpulse(pPower);
    }
}
