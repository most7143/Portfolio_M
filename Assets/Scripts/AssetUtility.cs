using System.IO;
using UnityEditor;
using UnityEngine;

public static class AssetUtility
{
#if UNITY_EDITOR

    public static void RenameAsset(ScriptableObject scriptableObject, string newName)
    {
        if (Application.isPlaying) return; // 게임이 실행 중이면 처리하지 않음

        try
        {
            // 현재 ScriptableObject의 파일 경로 가져오기
            string assetPath = AssetDatabase.GetAssetPath(scriptableObject);
            if (string.IsNullOrEmpty(assetPath))
            {
                throw new System.Exception("[AssetUtility] 에셋이 프로젝트에 존재하지 않음.");
            }

            string currentFileName = Path.GetFileNameWithoutExtension(assetPath);

            if (currentFileName == newName) return; // 이름이 동일하면 아무것도 하지 않음

            // 같은 이름이 존재하는지 확인
            string assetDir = Path.GetDirectoryName(assetPath);
            string newPath = assetDir + "/" + newName + ".asset";

            if (File.Exists(newPath))
            {
                throw new System.Exception($"[AssetUtility] 파일명이 이미 존재하여 변경하지 않습니다: {newName}");
            }

            // 에셋 임포트 중인지 체크
            if (AssetDatabase.IsAssetImportWorkerProcess())
            {
                throw new System.Exception("[AssetUtility] 현재 에셋이 임포트 중이므로 이름 변경을 생략합니다.");
            }

            // 파일 이름 변경
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();
        }
        catch (System.Exception e)
        {
            // 예외 발생 시 로깅 처리
            Debug.LogWarning(e.Message);
        }
    }

#endif
}