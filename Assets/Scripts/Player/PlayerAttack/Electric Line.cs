using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricLine : MonoBehaviour
{
	private LineRenderer line;
	public int segments = 10; 
	public float posJitter = 0.5f;
    public float activeTime = 0.3f;

	private void Awake()
	{
		line = GetComponent<LineRenderer>();
	}

    public void DrawElectric(Vector2 start, Vector2 end)
	{
        this.gameObject.SetActive(true);
        Vector2 set = end - start;
        set = set.normalized;
        start += set;
        StartCoroutine(DrawElectricRoutine(start, end));
	}
    

    private IEnumerator DrawElectricRoutine(Vector2 start, Vector2 end)
    {
        line.positionCount = segments;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 pos = Vector3.Lerp(start, end, t);

            if (i > 0 && i < segments - 1)
            {
                pos += Random.insideUnitSphere * posJitter;
            }

            line.SetPosition(i, pos);
        }

        yield return new WaitForSeconds(activeTime);

        this.gameObject.SetActive(false);
    }

}
