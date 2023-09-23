using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    [SerializeField] private bool _isLoop;

    [SerializeField] private AudioClip[] _initClips;

    private Queue<AudioClip> _queue = new();

    private void Awake()
    {
        foreach (AudioClip clip in _initClips)
            EnqueueMusic(clip);
    }

    public void Update()
    {
        if (!_source.isPlaying)
        {
            var clip = _queue.Dequeue();
            _source.clip = clip;
            _source.Play();
            if (_isLoop) EnqueueMusic(clip);
        }
    }

    public void EnqueueMusic(AudioClip clip)
    {
        _queue.Enqueue(clip);
    }
}
