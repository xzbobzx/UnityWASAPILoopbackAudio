# UnityWASAPILoopbackAudio
A barebones example of WASAPI Loopback Audio in Unity using CSCore

Check out CSCore:
https://github.com/filoe/cscore

Instructions:

1: Clone / download

2: Open in Unity

3: IMPORTANT! If you are going to be making a build using this, you need to go to Edit -> Project Settings -> Player -> Api Compatibility Level and set it to ".NET 2.0". Otherwise you will get a kernel exception upon trying to run Unity Player.

4: Play some music on your computer

5: Bang that "Play" button

That's it!

You won't see the bar analyzer until you hit Play - it's instantiated at runtime.

--

To use VLC filename http interfacing, set your VLC desktop shortcut target to:

"[path/vlc.exe]" --intf qt --extraintf=http --http-password password
