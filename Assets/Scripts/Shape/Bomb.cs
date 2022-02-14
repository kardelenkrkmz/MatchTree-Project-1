<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bomb : Shape
{
    public override void Explode()
    {

    }

    public override void Merge()
    {
        throw new System.NotImplementedException();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        Explode();
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Shape
{
    public override void Explode()
    {
        throw new System.NotImplementedException();
    }
}
>>>>>>> Stashed changes
