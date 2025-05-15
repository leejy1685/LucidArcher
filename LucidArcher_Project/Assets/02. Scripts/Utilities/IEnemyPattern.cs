using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IEnemyPattern
{
    MonsterBase Monster { get; set; }

    public void Init(MonsterBase monster);
    public void Execute(Action actionAfterExecute);    
}
