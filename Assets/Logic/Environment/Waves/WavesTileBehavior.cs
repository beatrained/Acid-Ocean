using Cinemachine;
using UnityEngine;

namespace Assets.Logic.Environment.Waves
{
    public class WavesTileBehavior : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        private Vector2 _prevCamPos;

        [SerializeField] private GameObject[] _wavesFirstGroup = new GameObject[2];
        [SerializeField] private GameObject[] _wavesSecondGroup = new GameObject[2];
        private GameObject[] _wavesActiveGroup;
        private GameObject[] _wavesInactiveGroup;
        private float _waveLength = 78;//27.2f;

        private Vector2 _camBoundaries = new Vector2(70, 20);

        private void Start()
        {
            _prevCamPos.x = _camera.transform.position.x;
            _prevCamPos.y = _camera.transform.position.y;
            _wavesActiveGroup = _wavesFirstGroup;
            _wavesInactiveGroup = _wavesSecondGroup;
        }

        private void Update()
        {
            if (_camera.transform.position.x == 0 || _camera.transform.position.y == 0)
            {
                return;
            }

            if (Mathf.RoundToInt(_camera.transform.position.x) % _camBoundaries.x == 0)
            {
                if (_camera.transform.position.x > _prevCamPos.x)
                {
                    float newPosX = (_camera.transform.position.x - (_camera.transform.position.x % _waveLength)) + _waveLength;
                    foreach (var gameObj in _wavesInactiveGroup)
                    {
                        //gameObj.transform.Translate(_waveLength, 0, 0);
                        gameObj.transform.position = new Vector3(newPosX, gameObj.transform.position.y, gameObj.transform.position.z);
                    }
                    (_wavesActiveGroup, _wavesInactiveGroup) = (_wavesInactiveGroup, _wavesActiveGroup);
                }
                else if (_camera.transform.position.x < _prevCamPos.x)
                {
                    float newPosX = _camera.transform.position.x - (_camera.transform.position.x % _waveLength);
                    foreach (var gameObj in _wavesInactiveGroup)
                    {
                        //gameObj.transform.Translate(-_waveLength, 0, 0);
                        //gameObj.transform.position -= new Vector3(_waveLength, 0, 0);
                    }
                    (_wavesActiveGroup, _wavesInactiveGroup) = (_wavesInactiveGroup, _wavesActiveGroup);
                }
                _prevCamPos = _camera.transform.position;
            }
        }
    }
}