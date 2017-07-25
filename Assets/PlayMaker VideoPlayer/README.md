PlayMaker--VideoPlayer
================

## Licensing
This package is released under LGPL license: [http://opensource.org/licenses/LGPL-3.0](http://opensource.org/licenses/LGPL-3.0)


## Description
This is a proxy component forwarding `VideoPlayer` events as PlayMaker events.

## Unity Documentation
It's important to read the Unity documentation related to all these events to understand them and what to expect from them in terms of data: check the `Event` section on the link below:

[https://docs.unity3d.com/ScriptReference/Video.VideoPlayer.html]()


## Implementation
- Drop `PlayMakerVideoPlayerEventsProxy` on any GameObjects  
- You can target VideoPlayers using the "Owner" property  
- Leave to none all `VideoPlayer` events you don't want to use
- Select global events for each `VideoPlayer` Events you want.
- Use `GetEventInfo` to retrieve data from `VideoPlayer` events

