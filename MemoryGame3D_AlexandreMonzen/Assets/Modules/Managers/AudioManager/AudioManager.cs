using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Monzen.Modules.Audio
{
    public sealed class AudioManager : MonoBehaviour
    {
        private AudioSource _globalAudioSource2D;

        [Header("Audio Source 3D pool")]
        [SerializeField] private GameObject _audioSource3dObj;
        [SerializeField] private int _amountAudioSource3d = 30;
        private List<AudioSource> _pooledAudioSource3d;

        private int _randomNumber;
        private AudioClip _randomAudioClip;

        private void Awake()
        {
            this.transform.position = Vector3.zero;
            _globalAudioSource2D = transform.GetChild(0).GetComponent<AudioSource>();

            CreateAudioSource3dPool();
        }

        ///<summary>
        ///Play a global 2D sound
        ///</summary>
        ///<param name="audioClip"> Audio clip that will be played </param>
        public void PlayAudio2D(AudioClip audioClip)
        {
            _globalAudioSource2D.PlayOneShot(audioClip);
        }

        ///<summary>
        ///Play a 3D sound at a specified position
        ///</summary>
        ///<param name="audioClip"> Audio clip that will be played </param>
        ///<param name="pointOfOrigin"> Position where the audio clip will be played </param>
        public void PlayAudio3D(AudioClip audioClip, Vector3 pointOfOrigin)
        {
            AudioSource pooledAudioSource = GetPooledAudioSource3D();
            pooledAudioSource.transform.position = pointOfOrigin;

            pooledAudioSource.gameObject.SetActive(true);
            pooledAudioSource.PlayOneShot(audioClip);

            StartCoroutine(DeactiveAudioSource3D(pooledAudioSource, audioClip.length));
        }

        ///<summary>
        ///Play a 3D sound at a specified position with a random pitch change
        ///</summary>
        ///<param name="audioClip"> Audio clip that will be played </param>
        ///<param name="pointOfOrigin"> Position where the audio clip will be played </param>
        ///<param name="pitchChangeRange"> Add a randomly value between negative and positive of pitchChangeRange value to the pitch </param>
        public void PlayAudio3D(AudioClip audioClip, Vector3 pointOfOrigin, float pitchChangeRange)
        {
            AudioSource pooledAudioSource = GetPooledAudioSource3D();
            pooledAudioSource.transform.position = pointOfOrigin;

            pooledAudioSource.pitch += (Random.Range(-pitchChangeRange, +pitchChangeRange));
            pooledAudioSource.pitch = Mathf.Clamp(pooledAudioSource.pitch, -3f, 3f);

            pooledAudioSource.gameObject.SetActive(true);
            pooledAudioSource.PlayOneShot(audioClip);

            StartCoroutine(DeactiveAudioSource3D(pooledAudioSource, audioClip.length));
        }

        ///<summary>
        ///Play a 3D sound at a specified position with a random pitch change
        ///</summary>
        ///<param name="audioClip"> Audio clip that will be played </param>
        ///<param name="pointOfOrigin"> Position where the audio clip will be played </param>
        ///<param name="pitchChangeRange"> Add a randomly value between negative and positive of pitchChangeRange value to the pitch </param>
        ///<param name="volume"> Set the audio clip volume </param>
        public void PlayAudio3D(AudioClip audioClip, Vector3 pointOfOrigin, float pitchChangeRange, float volume)
        {
            AudioSource pooledAudioSource = GetPooledAudioSource3D();
            pooledAudioSource.transform.position = pointOfOrigin;

            pooledAudioSource.pitch += (Random.Range(-pitchChangeRange, +pitchChangeRange));
            pooledAudioSource.pitch = Mathf.Clamp(pooledAudioSource.pitch, -3f, 3f);

            pooledAudioSource.volume = volume;

            pooledAudioSource.gameObject.SetActive(true);
            pooledAudioSource.PlayOneShot(audioClip);

            StartCoroutine(DeactiveAudioSource3D(pooledAudioSource, audioClip.length));
        }

        ///<summary>
        ///Play a random 3D sound from a array of AudioClip at a specified position
        ///</summary>
        ///<param name="audioClips"> Audio clip array that will have one random audio clip to play </param>
        ///<param name="pointOfOrigin"> Position where the audio clip will be played </param>
        public void PlayRandomAudio3D(AudioClip[] audioClips, Vector3 pointOfOrigin)
        {
            AudioSource pooledAudioSource = GetPooledAudioSource3D();
            pooledAudioSource.transform.position = pointOfOrigin;

            _randomAudioClip = ReturnRandomClip(audioClips);

            pooledAudioSource.gameObject.SetActive(true);
            pooledAudioSource.PlayOneShot(_randomAudioClip);

            StartCoroutine(DeactiveAudioSource3D(pooledAudioSource, _randomAudioClip.length));
        }

        ///<summary>
        ///Play a random 3D sound from a array of AudioClip at a specified position
        ///</summary>
        ///<param name="audioClips"> Audio clip array that will have one random audio clip to play </param>
        ///<param name="pointOfOrigin"> Position where the audio clip will be played </param>
        ///<param name="pitchChangeRange"> Add a randomly value between negative and positive of pitchChangeRange value to the pitch </param>
        public void PlayRandomAudio3D(AudioClip[] audioClips, Vector3 pointOfOrigin, float pitchChangeRange)
        {
            AudioSource pooledAudioSource = GetPooledAudioSource3D();
            pooledAudioSource.transform.position = pointOfOrigin;

            pooledAudioSource.pitch += (Random.Range(-pitchChangeRange, +pitchChangeRange));
            pooledAudioSource.pitch = Mathf.Clamp(pooledAudioSource.pitch, -3f, 3f);

            _randomAudioClip = ReturnRandomClip(audioClips);

            pooledAudioSource.gameObject.SetActive(true);
            pooledAudioSource.PlayOneShot(_randomAudioClip);

            StartCoroutine(DeactiveAudioSource3D(pooledAudioSource, _randomAudioClip.length));
        }

        ///<summary>
        ///Play a random 3D sound from a array of AudioClip at a specified position
        ///</summary>
        ///<param name="audioClips"> Audio clip array that will have one random audio clip to play </param>
        ///<param name="pointOfOrigin"> Position where the audio clip will be played </param>
        ///<param name="pitchChangeRange"> Add a randomly value between negative and positive of pitchChangeRange value to the pitch </param>
        ///<param name="volume"> Set the audio clip volume </param>
        public void PlayRandomAudio3D(AudioClip[] audioClips, Vector3 pointOfOrigin, float pitchChangeRange, float volume)
        {
            AudioSource pooledAudioSource = GetPooledAudioSource3D();
            pooledAudioSource.transform.position = pointOfOrigin;

            pooledAudioSource.pitch += (Random.Range(-pitchChangeRange, +pitchChangeRange));
            pooledAudioSource.pitch = Mathf.Clamp(pooledAudioSource.pitch, -3f, 3f);

            pooledAudioSource.volume = volume;

            _randomAudioClip = ReturnRandomClip(audioClips);

            pooledAudioSource.gameObject.SetActive(true);
            pooledAudioSource.PlayOneShot(_randomAudioClip);

            StartCoroutine(DeactiveAudioSource3D(pooledAudioSource, _randomAudioClip.length));
        }

        ///<summary>
        ///Play a random 3D sound from a array of AudioClip at a specified position
        ///</summary>
        ///<param name="audioClips"> Audio clip array that will have one random audio clip to play </param>
        ///<param name="pointOfOrigin"> Position where the audio clip will be played </param>
        ///<param name="pitchChangeRange"> Add a randomly value between negative and positive of pitchChangeRange value to the pitch </param>
        ///<param name="minDistance"> Outside the min distance the volume starts to attenuate </param>
        ///<param name="maxDistance"> MaxDistance is the distance a sound stops attenuating at </param>
        public void PlayRandomAudio3D(AudioClip[] audioClips, Vector3 pointOfOrigin, float pitchChangeRange, float minDistance, float maxDistance)
        {
            AudioSource pooledAudioSource = GetPooledAudioSource3D();
            pooledAudioSource.transform.position = pointOfOrigin;

            pooledAudioSource.pitch += (Random.Range(-pitchChangeRange, +pitchChangeRange));
            pooledAudioSource.pitch = Mathf.Clamp(pooledAudioSource.pitch, -3f, 3f);

            pooledAudioSource.minDistance = minDistance;
            pooledAudioSource.maxDistance = maxDistance;

            _randomAudioClip = ReturnRandomClip(audioClips);

            pooledAudioSource.gameObject.SetActive(true);
            pooledAudioSource.PlayOneShot(_randomAudioClip);

            StartCoroutine(DeactiveAudioSource3D(pooledAudioSource, _randomAudioClip.length));
        }

        private AudioClip ReturnRandomClip(AudioClip[] audioClips)
        {
            _randomNumber = Random.Range(0, audioClips.Length);
            return audioClips[_randomNumber];
        }

        #region Audio Source 3D Pool
        private void CreateAudioSource3dPool()
        {
            _pooledAudioSource3d = new List<AudioSource>();
            for (int i = 0; i < _amountAudioSource3d; i++)
            {
                GameObject obj = Instantiate(_audioSource3dObj);
                obj.SetActive(false);
                _pooledAudioSource3d.Add(obj.GetComponent<AudioSource>());
            }
        }

        private AudioSource GetPooledAudioSource3D()
        {
            for (int i = 0; i < _pooledAudioSource3d.Count; i++)
            {
                if (!_pooledAudioSource3d[i].gameObject.activeInHierarchy)
                    return _pooledAudioSource3d[i];
            }
            return null;
        }

        private IEnumerator DeactiveAudioSource3D(AudioSource audioSource3D, float timeToDeactive)
        {
            yield return new WaitForSeconds(timeToDeactive);
            audioSource3D.pitch = 1;
            audioSource3D.maxDistance = 10;
            audioSource3D.minDistance = 15;
            audioSource3D.volume = 1;
            audioSource3D.gameObject.SetActive(false);
        }
        #endregion
    }
}