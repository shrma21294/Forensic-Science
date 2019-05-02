#pragma strict

var tfLight: Transform;
 
function Start () {
    // find the revealing light named "RevealingLight":
    var goLight = GameObject.Find("RevealingLight");
    if (goLight) tfLight = goLight.transform;
}
 
function Update () {
    if (tfLight){
        GetComponent.<Renderer>().material.SetVector("_LightPos", tfLight.position);
        GetComponent.<Renderer>().material.SetVector("_LightDir", tfLight.forward);
    }
}