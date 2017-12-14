Prototype PlayMaker Integration
================

## Licensing
This package is released under [MIT license](https://github.com/inkle/prototype/blob/master/LICENSE)

Original work: https://github.com/inkle/prototype

## Description

The `Prototype` Unity pattern - an alternative to the standard Prefab workflow

Drop a `Prototype` component on a GameOject in your scene and it becomes a Pool for that GameObject. That original GameObject will be disabled on start.

* use the action `PrototypeCreateGameObject` to instance that *Prototype*
* use the action `PrototypeReturnToPool` to return a given instance back to the pool ( it gets disabled)
* use the action `PrototypeOnReturnToPoolEvent` to listen to the return callback. a one frame delay is introduced if you are listening to the retrun callback on the instance itself, leaving you that frame to act before the gameobject gets disabled.

## Requirements

You need PlayMaker :)

## Mods
The version of `Prototype` distributed here has several modifications. 

* Namespace: the script is properly namespaces under Com.InkleStudios to avoid conflicts with other thirdparties that may also have a Prototype class.
* a New public bool property OneFrameReturnDelay was introduced to delay the disabling of a returned instance by one frame, thus leaving one frame for the instance to act upon the return. It make things easier for PlayMaker integration this way.


## Support
If you have question, please use the [PlayMaker forum](http://hutonggames.com/playmakerforum/index.php)
