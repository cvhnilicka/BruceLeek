using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeController : MonoBehaviour
{
    //readonly Dictionary<int, int> HeatlhTree = new Dictionary<int, int>({0:10,1:15});
    int[] HealthTree = new int[] { 10, 15, 20, 25 };
    private int currentHealthLevel;
    public readonly int MaxHealthLevel = 3;
    // Start is called before the first frame update
    void Start()
    {
       
        currentHealthLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetHealthTreeAmount()
    {
        print("CUrrent Health Level: " + this.currentHealthLevel);
        //print(this.HeatlhTree[0]);
        return this.HealthTree[currentHealthLevel];
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
