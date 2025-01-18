using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class StageManager : MonoBehaviour
    {
        public Stage CurrentStage;

        public StageData Data;

        public int ChangeLevel = 3;

        public void ChangeStage(int monsterLevel)
        {
            int divi = (monsterLevel - 1) / ChangeLevel;

            if (divi >= (int)CurrentStage.Name)
            {
                if (CurrentStage.Name + 1 != StageNames.End)
                {
                    Spawn(CurrentStage.Name + 1);
                }
            }
        }

        public void Spawn(StageNames stageName)
        {
            Despawn();

            GameObject stage = ResourcesManager.Instance.Load(stageName);

            stage.transform.SetParent(transform);

            CurrentStage = stage.GetComponent<Stage>();

            Data = ResourcesManager.Instance.LoadScriptable<StageData>(stageName.ToString());

            if (Data != null)
            {
                UIManager.Instance.StageInfo.Refresh(Data);
            }
        }

        public void Despawn()
        {
            if (CurrentStage != null)
            {
                Destroy(CurrentStage.gameObject);
            }
        }
    }
}