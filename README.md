**virus2spread virus distribution model-simulation**

Here arises an attempt to test DR Clare Craig's hypothesis about airborne virus contagion -> Book Expired.
<br> 
To investigate annually recurring waves from the momentum of a chaotic system, and study or play with it's dynamics.
<br> 
Since the linear sientific SIR model used, seems to differ from reality.
<br>
<br> 
<br>So what is special about this simulation?
<br>Most other simulations already assume the droplet theory, this one does not. 
<br> 
<br>Further information see ![virus2spread wiki](https://github.com/gitfrid/virus2spread/wiki)
<br> 
 Reproduction is not yet implemented - a long way to go.
<br> However, I hope it will be useful for others.
<br>For maximal Iteration speed minimize the render Simulation Window to the Windows Taskbar.
<br>
<br>
![Virus2spread_screenshot](https://github.com/gitfrid/virus2spread/assets/148685307/44b1183b-f772-417a-aea1-ddfd1f437de1)
<br>
<br>
<br>
**Installation:**
<br>
<br>**Prerequisite: Microsoft .Net 8 has to be installed first!** https://dotnet.microsoft.com/en-us/download
<br>
<br>Download the ![Virus2SpreadSetup.msi](https://github.com/gitfrid/virus2spread/blob/76d5b8504ea72f10f45b8decb091b733b8b69358/virus2spreadSetup/Release/virus2spreadSetup.msi) to install the Software.
<br>In github folder ![virus2spreadSetup/Release/](https://github.com/gitfrid/virus2spread/tree/76d5b8504ea72f10f45b8decb091b733b8b69358/virus2spreadSetup/Release)
<br>Thumbprint self signd certificate:f28154fafc6a8a540a3cf9527a05c81ba7a7fd58
<br>
<br>Alternative, it is  sufficient if only the binary files are copied fom this github folder: 
<br>/Virus2spread/bin/Release/net8.0-windows7.0/*.*  to any folder, and double click Virus2spread.exe to run the Simulation.
<br>
<br>**Attention!!!**
<br>A security prompt **"The computer has been protected by Windows"** or **"The publisher is unknown"** appears, when you first run the setup or the software.
This is because the software is not digitally signed, i.e. the publisher of the software is unknown and not officially certified.
<br>
<br>
C# : Net 8.0 WindowsForms, Nuget Packages : 
<br> 
Serilog Console
<br> 
Skia skglControl
<br> 
SharpSerializer.Net Core
<br>
Scottplot
<br>
<br>
License: open source - MIT License
<br>
Disclaimer: Just a simulation model, for use out of pure curiosity, not to force people.



