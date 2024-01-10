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

    public void Init()
    {
        
    }

    public void AA()
    {
        if (growthType == GrowthType.Training)
        {
            var a = DefaultTable.Training.TrainingList.FirstOrDefault(x => x.GrowthType == growthType &&
                                                                           x.TrainingGrade == trainingGrade &&
                                                                           x.TrainingType == trainingType);
            if (PlayerManager.I.attPowerLv < a.MaxLevel)
            {
                PlayerManager.I.attPowerLv++;
            }
        }
        else if (growthType == GrowthType.Ability)
        {
            
        }
    }
}
