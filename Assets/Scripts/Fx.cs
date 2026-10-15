using UnityEngine;

public static class Fx
{
    static GameObject burstPrefab;

    public static void Burst(Vector3 position, Color color, float size)
    {
        if (burstPrefab == null)
        {
            burstPrefab = Resources.Load<GameObject>("Prefabs/HitBurst");
        }
        GameObject burst = Object.Instantiate(burstPrefab, position, Quaternion.identity);
        ParticleSystem ps = burst.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = ps.main;
        main.startColor = color;
        main.startSize = 0.25f * size;
    }
}
