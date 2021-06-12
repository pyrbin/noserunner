using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

public static class CameraUtils
{
    public static CinemachineBrain MainBrain()
    {
        Camera.main.transform.TryGetComponent<CinemachineBrain>(out var brain);
        return brain;
    }
}
