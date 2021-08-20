﻿using UnityEngine;

namespace PlatformerMVC
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private SpriteAnimatorConfig _playerConfig;
        [SerializeField] private int _animationSpeed=10;
        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private GeneratorLevelView _genView;
        private SpriteAnimatorController _playerAnimator;
        private PlayerController _playerController;
        private CameraController _cameraController;
        private CannonAimController _cannon;
        private BulletEmitterController _bulletEmitterController;
        private GeneratorController _levelGenerator;
        void Awake()
        {
            _playerConfig = Resources.Load<SpriteAnimatorConfig>("PlayerAnimCfg");
            _playerAnimator = new SpriteAnimatorController(_playerConfig);
            _playerController = new PlayerController(_playerView, _playerAnimator);
            _cameraController = new CameraController(_playerView._transform, Camera.main.transform);
            _cannon = new CannonAimController(_cannonView._muzzleTransform, _playerView._transform);
            _bulletEmitterController = new BulletEmitterController(_cannonView._bullets, _cannonView._emitterTransform);
            _levelGenerator = new GeneratorController(_genView);

            _levelGenerator.Init();
        }

        void Update()
        {
            _playerController.Update();
            _cameraController.Update();
            _cannon.Update();
            _bulletEmitterController.Update();
        }

        private void FixedUpdate()
        {
            
        }
    }
}