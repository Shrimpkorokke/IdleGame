using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GrowthButton : MonoBehaviour
{
    [GetComponentInChildren] public Button btn;
    public GrowthType growthType;
    public TrainingGrade trainingGrade;
    public TrainingType trainingType;
    public int TID;
    public void Init()
    {
        btn.onClick.AddListener(()=>PlayerManager.I.IncreaseGrowth(this));
    }
}
