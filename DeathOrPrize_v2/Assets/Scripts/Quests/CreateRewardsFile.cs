using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRewardsFile : MonoBehaviour
{
    public List<ItemModel> items;
    public string idRewads;
    void Start()
    {
        DataFileController dfc = new DataFileController();
        List<RewardModel> rewards = GetRewardsList();
        dfc.SaveEncrypted<List<RewardModel>>(rewards, PathHelper.RewardsDataFile(idRewads));
    }
    private List<RewardModel> GetRewardsList()
    {
        List<RewardModel> rewards = new List<RewardModel>();
        for (int x = 0; x < items.Count; x++)
        {
            RewardModel reward = new RewardModel();
            reward.idKingdom = x + 1;
            reward.reward = items[x];
            rewards.Add(reward);
        }

        return rewards;
    }
}
