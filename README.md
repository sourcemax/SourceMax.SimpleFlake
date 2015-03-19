# SourceMax.SimpleFlake
A .NET implementation of the SimpleFlake concept for generating uncoordinated, distributed, k-ordered IDs at scale.

This is especially useful in cloud computing scenarios where multiple machines need to create IDs in a distributed 
setting without the need of coordinating these IDs with other sources. Ideally, these IDs should be roughly sortable
based on date and time that they are created and should minimize the risk of collisions.

This solution generates a 64-bit number where the first 41 bits are the number of milliseconds from some epoch 
(2000-01-01, by default), and the last 23 bits are random "salt" to minimize collision risk.

Base (Unencoded) Implementation
-------

This simple project provides a generic, base implementation for generating Flakes that returns a raw BigInteger
value that can encoded any desired technique.

```c#
using SourceMax.SimpleFlake;
...
var generator = new FlakeGenerator();
var flake = generator.CreateFlake<Flake>();
var id = flake.ToString();
// id is a big, long number e.g. "4027488528651508814"
```

Base-58 Implementation
-------

The Base-58 Flake's ToString method encodes the BigInteger based on these characters: 123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz.
The resulting encoded string IDs are considered k-ordered because they are roughly sortable based on the date and time they were created.
And Base-58 strings are more readable by elimating the zero, capital "I", capital "O" and lowercase "L".

```c#
var generator = new Base58.FlakeGenerator();
string id = generator.CreateFlake().ToString();
// id is a 12-char string like...
// "1AMEGVoagDg7" at t1 or ...
// "1AMEGvNA5RVM" at t2 (appx 1 minute later)
```

Attribution
-------

Inspired by the SawdustSoftware/simpleflake project and these other people and references:

* [Sawdust Blog Entry](http://engineering.custommade.com/simpleflake-distributed-id-generation-for-the-lazy/)
* [SawdustSoftware/simpleflake GitHub Project (Python)](https://github.com/SawdustSoftware/simpleflake)
* [Simon Ratner node-simpleflake GitHub Project (NodeJs)](https://github.com/simonratner/node-simpleflake)
* [CodesInChaos Base 58 Encoding](https://gist.github.com/CodesInChaos/3175971)
* [Mali Akmanalp](https://twitter.com/makmanalp)


License
-------

The MIT License (MIT)

Copyright (c) 2015 Chris Wiederspan

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
