using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class StageManager : MonoBehaviour
    {
        public Stage CurrentStage;

        public StageData Data;

        public void Spawn(StageNames stageName)
        {
            GameObject stage = Instantiate(Resources.Load<GameObject>("Prefabs/Stage/" + stageName));

            stage.transform.SetParent(transform);

            CurrentStage = stage.GetComponent<Stage>();
            Data = Resources.Load<StageData>("ScriptableObject/Stage/" + stageName);

            if (Data != null)
            {
                InGameManager.Instance.StageInfo.Refresh(Data);
            }
        }

        public void Despawn()
        {
            if (CurrentStage != null)
            {
            }
        }
    }
}