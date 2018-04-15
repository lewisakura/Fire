<h1 align="center">🔥 Fire</h1>
<h3 align="center">Kill it with Fire!</h3>
<p align="center">
    <a href="https://ci.appveyor.com/project/LewisTehMinerz/Fire">
        <img src="https://img.shields.io/appveyor/ci/LewisTehMinerz/Fire.svg?style=for-the-badge&logo=appveyor">
    </a>
</p>

Fire is a small tool developed to make it easy to kill process trees. All you need to do is select a window, press SHIFT+F4, and click "Yes".

### Why?
Fire was developed because of a small issue I had. Whenever I ALT+F4 an application, I would expect all subprocesses to die. In some applications, this is not the case. Sometimes, applications do not correctly clean up themselves when they are closed, which means that subprocesses can linger around longer than they should. Fire forcefully closes the parent process and all subprocesses, which is quite useful in the case of a program malfunctioning and spawning a lot of subprocesses.

### Downloads
You can download beta builds of Fire from [AppVeyor](https://ci.appveyor.com/project/LewisTehMinerz/Fire).

You can download stable releases from the [Releases](https://github.com/LewisTehMinerz/Fire/releases) page.

### Special Thanks

<p>
    <a href="https://jetbrains.com/resharper/">
        <img src="http://resources.jetbrains.com/storage/products/resharper/img/meta/resharper_logo_300x300.png" alt="ReSharper" width="100" align="right">
    </a>
    Special thanks to Jetbrains for their ReSharper tool. It's used to help build Fire. I highly recommend it to anyone who is coding in .NET and people who code in C++ because there's a version for you C++ developers!
</p>
<p>
    Thanks to Hedley Muscroft for his <a href="https://www.codeproject.com/Articles/21276/Vista-TaskDialog-Wrapper-and-Emulator">PSTaskDialog Library</a>.
</p>