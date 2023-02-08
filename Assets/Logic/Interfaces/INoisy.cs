using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INoisy
{
    public bool Noise { get; set; }

    public IEnumerator MakeNoise(float time)
    {
        Noise = true;
        yield return new WaitForSeconds(time);
        Noise = false;
    }
}
