using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemFluidTrail : MonoBehaviour
{
    public ParticleSystem ps;
    public int numOfLineRenderers = 20;
    private LineRenderer[] lrArray;
    public Transform lrGroupTransform;
    public List<int> ribbonBreaks;
    public float maxParticleRibbonBreakDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        ribbonBreaks = new List<int>();

        lrArray = new LineRenderer[20];
        lrArray[0] = lrGroupTransform.GetComponentInChildren<LineRenderer>();
        for (int i = 1; i < lrArray.Length; i++)
        {
            lrArray[i] = Instantiate(lrArray[0]);
            lrArray[i].transform.parent = lrGroupTransform;
            lrArray[i].useWorldSpace = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[ps.particleCount];
        ps.GetParticles(particles);       

        Vector3[] positions = new Vector3[particles.Length];
        System.Array.Sort(positions, (x, y) => Vector3.Distance(x, transform.position).CompareTo(Vector3.Distance(y, transform.position)));

        ribbonBreaks.Clear();
        for (int i = 0; i < particles.Length; i++)
        {
            //positions[i] = particles[i].position;
            positions[i] = particles[i].position;
            if (i > 0 && Vector3.Distance(positions[i], positions[i - 1]) > maxParticleRibbonBreakDistance)
            {
                ribbonBreaks.Add(i);
            }
        }
        ribbonBreaks.Add(particles.Length);

        Debug.Log(string.Join(", ", ribbonBreaks));

        // Update the line renders to display the particle ribbons, and render breaks when adjacent particles get too far apart
        for (int i = 0; i < ribbonBreaks.Count && i < lrArray.Length; i++)
        {
            int rangeStartIdx = (i == 0) ? 0 : ribbonBreaks[i - 1];
            int rangeLength = ribbonBreaks[i] - rangeStartIdx;
            Debug.Log(string.Format("{0} --> {1}", rangeStartIdx, rangeStartIdx + rangeLength));
            Vector3[] tempPositions = new Vector3[rangeLength];
            System.Array.Copy(positions, rangeStartIdx, tempPositions, 0, rangeLength);

            lrArray[i].positionCount = rangeLength;
            lrArray[i].SetPositions(tempPositions);
        }

        // Make sure only updated line renders are visible
        for (int i = 1; i < lrArray.Length; i++)
        {
            bool isUsed = i <= ribbonBreaks.Count;
            lrArray[i].gameObject.SetActive(isUsed);
        }






    }
}
