using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IBuildable
{
    

    public void SetToHide();
    public void SetToAnimation();
    public void SetToBuild();
    public void SetToObstructed();
    public void Initialize();
    public void SetToPlace();
    public void SetScenarioRenderer(GameObject GO);
    public void SetCoordinates(int[] coordinates);

    public void Rotate();
    public void SetSpatial(Spatial spatial_parameter);

    

}
