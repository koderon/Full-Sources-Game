using UnityEngine;
using UnityEngine.UI;

namespace Gui
{

    public class UIGameReward : MonoBehaviour
    {
        [SerializeField] private Text _rewardText;

        public void SetText(int reward)
        {
            _rewardText.text = reward.ToString();
        }
    }

}
