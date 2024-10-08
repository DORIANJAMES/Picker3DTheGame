using UnityEngine;

namespace Runtime.Commands.Level
{
    public class OnLevelLoaderCommand
    {

        private Transform _levelHolder;
        public OnLevelLoaderCommand(Transform levelHolder)
        {
            _levelHolder = levelHolder;
        }
        
        public void Execute(byte levelIndex)
        {
            var levelObject = Object.Instantiate(Resources.Load<GameObject>($"Prefabs/LevelPrefabs/Level-{levelIndex}"), _levelHolder, true);
        }
    }
}