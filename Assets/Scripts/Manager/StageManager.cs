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

            Debug.Log(monsterLevel + " / " + divi);

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

            GameObject stage = Instantiate(Resources.Load<GameObject>("Prefabs/Stage/" + stageName));

            stage.transform.SetParent(transform);

            CurrentStage = stage.GetComponent<Stage>();

            Data = Resources.Load<StageData>("ScriptableObject/Stage/" + stageName);

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