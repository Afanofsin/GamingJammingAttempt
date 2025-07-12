using UnityEngine;
using UnityEngine.Rendering;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager Instance;

    [SerializeField]
    private AudioSource _audioSourcePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this);
        }
    }

    public void PlayFXClip(AudioClip clip, Transform spawnPosition, float clipVolume,
        float pitchLowEnd = float.NaN, float pitchHighEnd = float.NaN)
    {
        // Spawn of audio source
        AudioSource audioSource = Instantiate(_audioSourcePrefab, spawnPosition.position, Quaternion.identity);

        // Assign parameters
        audioSource.clip = clip;
        audioSource.volume = clipVolume;

        // If pitch randomization is set up, apply it
        /*if (pitchLowEnd != float.NaN && pitchHighEnd != float.NaN)
        {
            audioSource.pitch = UnityEngine.Random.Range(pitchLowEnd, pitchHighEnd);
        }*/
        audioSource.Play();

        // Destroy after done playing
        float clipLength = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomFXClip(AudioClip[] clips, Transform spawnPosition, float clipVolume,
        float pitchLowEnd = float.NaN, float pitchHighEnd = float.NaN)
    {
        // Spawn of audio source
         AudioSource audioSource = Instantiate(_audioSourcePrefab, spawnPosition.position, Quaternion.identity);

        AudioClip clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        // Assign parameters
        _audioSourcePrefab.clip = clip;
        _audioSourcePrefab.volume = clipVolume;

        // If pitch randomization is set up, apply it
        if (pitchLowEnd != float.NaN && pitchHighEnd != float.NaN)
        {
            _audioSourcePrefab.pitch = UnityEngine.Random.Range(pitchLowEnd, pitchHighEnd);
        }
        _audioSourcePrefab.Play();

        // Destroy after done playing
        float clipLength = _audioSourcePrefab.clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }
}
