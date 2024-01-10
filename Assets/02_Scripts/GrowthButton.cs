using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrowthButton : MonoBehaviour
{
    [GetComponentInChildren] public Button btn;
    public GrowthType growthType;
    public TrainingGrade trainingGrade;
    public TrainingType trainingType;

    public void Init()
    {
        
    }
}
