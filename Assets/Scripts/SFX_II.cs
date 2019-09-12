using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;



public class SFX_II : MonoBehaviour
{

    public static SFX_II instance;

    public AudioMixer mixer;

    public void Awake(){
        if (instance != null){
            Destroy(gameObject);
        } else instance = this;

        PopulateLists();

        mixer = Resources.Load("Master") as AudioMixer;
    }

    [System.Serializable]
    public class SoundArray {

        public SoundArray(string name){
            this.name = name;
        }

        public string name;
        public List<AudioSource> sources = new List<AudioSource>();
    }

    public List<SoundArray> soundArrays = new List<SoundArray>();

    public void ClearLists(){
        foreach(SoundArray soundArray in soundArrays){
            soundArray.sources.Clear();
        }
    }

    public void PopulateLists(){

        soundArrays.Clear();

        int children = transform.childCount;

        for (int i = 0; i < children; i++){

            Transform child = transform.GetChild(i);
            SoundArray soundArray = new SoundArray(child.name);

            foreach(AudioSource source in child.GetComponents<AudioSource>()){
                
                source.playOnAwake = false;
                source.outputAudioMixerGroup = mixer.FindMatchingGroups("Sfx")[0];
                soundArray.sources.Add(source);
            }

            soundArrays.Add(soundArray);           
        }

        foreach(SoundArray soundArray in soundArrays){
            //Debug.Log(soundArray.name + " contains ");

            foreach(AudioSource source in soundArray.sources){
                //Debug.Log(source.name);
            }

        }
    }

    public void Play(string sound){

        if (instance == null){return;}

        foreach(SoundArray array in soundArrays){

            if (array.name.Equals(sound)){
                PlayFromList(array.sources);
            }
        }
    }

    public AudioSource GetFirstSource(string sound)
    {
        AudioSource source = null;

        if (instance == null) { return source; }

        foreach (SoundArray array in soundArrays)
        {

            if (array.name.Equals(sound))
            {
                source = array.sources[0];
            }
        }

        return source;
    }

    public void PlayAll(string sound){

        if (instance == null){ return;}

        foreach(SoundArray array in soundArrays){
            if (array.name.Equals(sound)){
                PlayAllFromList(array.sources);
            }
        }
    }

    /* public void PlayHit(){
        PlayFromList(hitSounds);
    } */
    

    /* public void PlaySwing(){
        PlayFromList(swingSounds);
    } */

    /* public void PlayExplosion(){
        PlayFromList(explosionSounds);
    } */

    public void PlayFromList(List<AudioSource> list){
        int rand = Random.Range(0, list.Count);
        list[rand].Play();
    }

    public void PlayAllFromList(List<AudioSource> list){
        foreach (AudioSource source in list){
            source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
