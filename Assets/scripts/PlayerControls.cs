using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {


    public float speed = 6.0f;
    public bool grounded = false;
    public bool movesInX = false;
    public bool movesInY = true;

    [SerializeField]
    float initialXPositionInUnits = 0;
    [SerializeField]
    private float initialYPositionInUnits = 0;

    public float pitch = 250; //100 - 500;
    public float loudness = 10;

    public float pitchMin = 100;
    public float pitchMax = 500;
    public float loudnessThreshold = 8;

    [SerializeField]
    private AnimationCurve waveStartingRadius = AnimationCurve.Linear(0, 1, 1, 0);
    [SerializeField]
    private AnimationCurve waveRadiusBehavior = AnimationCurve.Linear(0, 1, 1, 0);
    [SerializeField]
    private AnimationCurve waveLengthBehavior = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField]
    private AnimationCurve waveSpeedBehavior = AnimationCurve.Linear(0, 0, 1, 1);
    [SerializeField]
    private AnimationCurve waveCouldownBehavior = AnimationCurve.Linear(0, 1, 1, 0);
    [SerializeField]
    private AnimationCurve waveNumberBehavior = AnimationCurve.Linear(0, 1, 1, 1);
    [SerializeField]
    private AnimationCurve waveWidthBehavior = AnimationCurve.Linear(0, 1, 1, 1);

    private GameObject player;
    private ParticleSystem waveGenerator;
    private Vector3 moveDirection = Vector3.zero;
    private float nextWaveTimeMin = 0;
    private float nextWaveDisableTime = 0;
    
	// Use this for initialization
	void Start () {
        this.player = this.gameObject;
        this.player.transform.position = new Vector3(initialXPositionInUnits, 0, initialYPositionInUnits);
        this.waveGenerator = this.GetComponentInChildren<ParticleSystem>();
        this.waveGenerator.Play();
        var sh = this.waveGenerator.shape;
        sh.enabled = true;
        sh.shapeType = ParticleSystemShapeType.SingleSidedEdge;
}
	
	// Update is called once per frame
	void Update () {
        if (!grounded)
        {
            this.moveDirection = new Vector3(0, 0, 0);
            if (movesInX) this.moveDirection.x = Input.GetAxis("Horizontal");
            if (movesInY) this.moveDirection.y = Input.GetAxis("Vertical");
            this.moveDirection *= this.speed;
        }

        this.player.transform.Translate(this.moveDirection * Time.deltaTime);
        if (loudness >= loudnessThreshold && Time.time >= nextWaveTimeMin)
        {
            var emission = this.waveGenerator.emission;
            emission.enabled = true;

            float tmp = (pitch - pitchMin) / (pitchMax - pitchMin);
            // Length
            var main = this.waveGenerator.main;
            float lifeTime = waveLengthBehavior.Evaluate(tmp);
            main.startLifetime = lifeTime;
            // Radius
            var sizeOverLifetimeParam = this.waveGenerator.sizeOverLifetime;
            sizeOverLifetimeParam.enabled = true;
            //sizeOverLifetimeParam.y = waveRadiusBehavior.Evaluate(tmp);
            var sizeOverLifetime = new ParticleSystem.MinMaxCurve();
            var curve = new AnimationCurve();
            curve.AddKey(0, waveStartingRadius.Evaluate(tmp));
            curve.AddKey(lifeTime, waveRadiusBehavior.Evaluate(tmp));
            Debug.Log("lifeTime " + lifeTime);
            Debug.Log("waveRadiusBehavior.Evaluate(tmp) " + waveRadiusBehavior.Evaluate(tmp));
            sizeOverLifetimeParam.y = new ParticleSystem.MinMaxCurve(1, curve);
            // Width
            sizeOverLifetimeParam.x = waveWidthBehavior.Evaluate(tmp);
            // Speed
            main.startSpeed = waveSpeedBehavior.Evaluate(tmp);
            // Emission rate
            var emissionRate = emission.rateOverTime;
            emissionRate.constant = waveNumberBehavior.Evaluate(tmp);
            emission.rateOverTime = emissionRate;
            // CD
            nextWaveTimeMin = Time.time + waveCouldownBehavior.Evaluate(tmp);
            nextWaveDisableTime = Time.time + 1;
        }
        else if (this.waveGenerator.isPlaying && Time.time >= nextWaveDisableTime)
        {
            var emission = this.waveGenerator.emission;
            emission.enabled = false;
        }
	}
}
