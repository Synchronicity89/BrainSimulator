## Selected Brain Simulator Worlds

This section shows usage of selected Worlds in the Brain Simulator.

### AnimationPredictionWorld

This world is suitable for testing algorithms that should predict the sequences. The world reads the dataset in in the folowing format: `C:\absolutePath\NamePrefix_00000.png`. Then the world reads the ordered sequence and presents it to the output.

Compared to the `ImageDatasetWorld`, this also supports reloading the dataset online in the `Reload images` Task. This way, it is possible to change the sequence at runtime, without stopping or even pausing the simulation. 

![](../guides/img_examples/animationprediction.gif)


The dataset used in this example can be downloaded [here](../guides/img_examples/SwitchTest.zip).

### AssociativeNetworkWorld

Brain: [TextProcessing.brain](https://github.com/GoodAI/BrainSimulatorSampleProjects/blob/master/TextProcessing/AssociativeNetworkWorldExample.brain)

The `AssociativeNetworkWorld` serves for loading concepts and relations either from a file or typed in by the user. The information needs to be stored in an ASCII file in the comma separated format:
**Concept1**, **Concept2**, **Relation**, **relation strength** on each line. An example of a concept might be *"elephant"* or *"mouse"* and a relation might be *"is bigger than"*. The strength of this relation might be for example 0.9, because there sometimes exist really big mice in our world. We cam save this relation into a file as:

`elephant, mouse, is bigger than, 0.9`   

and load in into the Brain Simulator using `AssociativeNetworkWorld`. The world takes one line at a time from the input file and presents the information in the respective input blocks. The text is encoded as a vector of integers so we can perform various operations on it using existing nodes.

There are also two special nodes designed for manipulation with the text:

`TextInputNode`    can output a user defined constant string or can be used for translating an incoming number into a text vector.
`TextObserverNode` has it's own observer which can visualize the incoming strings or can be used for saving the strings into a file.

Utilization of the world and both nodes mentioned above is depicted in the image below. The row vectors (of strings encoded as integers) for both concepts and the relation are transposed and concatenated together with the information about the strength of the relation (which is beforehand translated into a text vector via `TextInputNode`). The resulting column text vector is transposed back into a row text vector and fed in the `TextObserverNode` for the purpose of visualization. The window of the custom observer of this node can be seen in the bottom of the image.

![](../img/TextProcessing/AssociativeNetworkWorld.PNG)

Second utilization of the `TextInputNode` is depicted in the second image. Three such nodes output three different strings as text vectors which are subsequently stacked together and visualized by the `TextObserverNode`. Notice the second input into this node (a vector of real numbers between 0 and 1) which determines the brightness of each line.  

![](../img/TextProcessing/TextNodes.PNG)

