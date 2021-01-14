using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeController : MonoBehaviour
{
    readonly Dictionary<int, int> HeatlhTree = new Dictionary<int, int>();
    private int currentHealthLevel;
    public readonly int MaxHealthLevel = 3;
    // Start is called before the first frame update
    void Start()
    {
        HeatlhTree.Add(0, 10);
        HeatlhTree.Add(1, 15);
        HeatlhTree.Add(2, 20);
        HeatlhTree.Add(3, 25);
        currentHealthLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetHealthTreeAmount()
    {
        return this.HeatlhTree[currentHealthLevel];
    }

    public bool IncreaseHealthTree()
    {
        if (currentHealthLevel < MaxHealthLevel)
        {
            currentHealthLevel += 1;
            return true;
        }
        return false;
    }
}
