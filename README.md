# FaceDetection
> A project for Microsoft C# Academy Course

### Description
Application can be used to locate human faces on JPG images and censor them. 

Face detection realised with EmguCV. EmguCV is a cross platform .Net wrapper to the OpenCV image processing library.


### Features
1. Detecting face areas in an image along with eyes, nose and mouth areas inside them. The result image may look like this:
![Example image](http://docs.opencv.org/trunk/face.jpg)

2. Applying censorship to these areas.

3. Logging the areas to an .xml file for further use.


### Notes
By default, the faces in an image should be at least 50x50 px in order to be detected.

Please note that used Haar Cascades and the OpenCV algorythms are far from perfect. The initial results might not be satisfying, thus the user needs to tweak the configuration such as scale factor, minimum of neighbors, minimal and maximal size of objects etc. User also might use his own Haar Cascades. Those used in the project belong to Intel Corporation.


### Installation
To get this to work you need following DLLs from EmguCV library:
* Emgu.CV.UI.dll
* Emgu.CV.World.dll

Download EmguCV: https://sourceforge.net/projects/emgucv/?source=typ_redirect

Copy the DLLs next to FaceDetection executable (eg. in bin\Debug folder).


### Project participant
Paweł Polewczak (polewczakp@gmail.com)
