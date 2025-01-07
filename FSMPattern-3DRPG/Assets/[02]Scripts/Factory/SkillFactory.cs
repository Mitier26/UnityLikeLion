using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class SkillFactory
{
    public static async UniTask<List<SkillInstance>> CreateSkill(Transform parent, List<string> skillNames)
    {
        List<SkillData> skillDatas = new List<SkillData>();

        foreach (string skillName in skillNames)
        {
            var handle = Addressables.LoadAssetAsync<SkillData>($"Assets/[06]SkillData/{skillName}.asset");
            
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                skillDatas.Add(handle.Result);
                
                Debug.Log($"Skill {skillName} has been loaded");
            }
            else
            {
                Debug.LogError($"Skill {skillName} could not be loaded");
            }
        }
        
        List<SkillInstance> skills = new List<SkillInstance>();
        
        foreach (var skillData in skillDatas)
        {
            var go = new GameObject(skillData.name);
            SkillInstance instance = go.AddComponent<SkillInstance>();
            go.transform.SetParent(parent);
            skills.Add(instance);
        }
        
        return skills;
    }
}