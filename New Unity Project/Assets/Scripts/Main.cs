using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private int _animationSpeed=10;
        [SerializeField] private LevelObjectView _playerView;
        private SpriteAnimatorController _playerAnimator;
        private PlayerController _playerController;
        void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            

            _playerController = new PlayerController(_playerView, _playerAnimator);
        }

        void Update()
        {
            _playerController.Update();
        }

        private void FixedUpdate()
        {
            
        }
    }
}