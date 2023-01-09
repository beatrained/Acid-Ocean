using UnityEngine;

namespace Assets.Logic.Environment.Waves
{
    public class WavesBehavior : MonoBehaviour
    {
        private GameObject[] _waves = new GameObject[2];

        [SerializeField] private float _waveSpeed = 0.5f;
        [SerializeField] private float _waveFrequency = 0.5f;
        [SerializeField] private float _waveAmplitude = 0.5f;
        [SerializeField, Range(-10, 10)] private float _waterLevel = 0;
        private Vector3 _wavePosition = Vector3.zero;
        private float _waveTravelDistMax = 20;
        private float _waveTravelDistMin = -20;

        [SerializeField] private bool _freezeMotion = false;

        private void Start()
        {
            _waves[0] = transform.Find("waves_1").gameObject;
            _waves[1] = transform.Find("waves_2").gameObject;
        }

        private void Update()
        {
            if (!_freezeMotion)
            {
                AnimateWaves();
            }
        }

        private void AnimateWaves()
        {
            for (int i = 0; i < _waves.Length; i++)
            {
                _wavePosition.x = _waveSpeed * Time.deltaTime * (i * 0.2f + 0.2f);  // magic numbers i != 0
                _wavePosition.y = Mathf.Sin(Time.time * _waveFrequency * (i * 0.2f + 0.2f)) * _waveAmplitude + _waterLevel;
                _waves[i].transform.position = new Vector3(_waves[i].transform.position.x + _wavePosition.x, _wavePosition.y, _waves[i].transform.position.z);
                if (_waves[i].transform.position.x >= _waveTravelDistMax || _waves[i].transform.position.x <= _waveTravelDistMin)
                {
                    _waveSpeed *= -1;
                }
            }
        }
    }
}