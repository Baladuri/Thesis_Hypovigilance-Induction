# Thesis_Hypovigilance-Induction
My Masters Thesis repository, Driving simulator game for the hypovigilance induction and detection 
The game has been developed in Unity 3D. Task was to design a scenario that would be able to induce states of hypovigilance (boredom, fatigue and sleep) in participants, and further the measurement and collection of the data for analysis and processing.

## Design Architecture
Various factors were considered for the design of the game given the simulation should be cognitively low stimulating
* An old style mini-truck was chosen as a vehicle, which I modelled and designed in the Blender 3D.
* The acceleration of the truck was set static with no breaks, so as to have no chance to increase or decrease the speed, thus boredom.
* Surrounding environment was built using Unity terrains, and was further sculpted so as to have monotony in both the structure and as well the ground texture (with a mossy glow). 
* Roads were designed using “easyroads3d”, an external asset package fromUnity, and the road architecture was kept mostly monotonous and straight, however, with some occasional turns and curves and some undulations.
* Other factors that were considered were light and sound. 
* To make the scene seem more hypnotising, a night-time shade was used for the surrounding sky, and also after some research in regards to sleep inducing light colours, a bluish light with luminous intensity was selected. 
* A random and very monotonous truck driving sound was also added on a loop in the background while driving. The sound also had a very hypnotic quality to it.
* The skybox of the scene was also modified as per the requirement, making the skylook  more  darker  with  the  touch  of  the  night  bluish  shade  ,  which  as  well  madethings seem more hypnotic.  A new material was created with specific adjustmentsto its attributes and added to the skybox.

## Experimental Design
The experiment was designed as per the neuropsychological test from Psychology called "Continuous Performance Task".
* Given the prerequisites of CPT, the experimental implementation of the task divides the game into three parts or experimental conditions, i.e., Hypovigilancecondition, High cognitive load condition, and the controlled Condition
* In all of the three conditions,  it was decided to have some sign posts as CPTtargets, distributed on the two sides of the road and each post either displaying across sign or a circle sign.
* The sign posts were created and modelledin Blender 3D.
* The posts were distributed manually across roads, and the total of 150 posts were added which approximates the timeinterval of 10 minutes for one experimental condition. 
* The CPT mechanism for theposts was carried out in a way that as the truck gets closer to the post, it displays either a cross or a circle sign, and accordingly the participant is supposed to respondas per the rules of the three conditions.
* To trigger a response from the participant, the participants are supposed to either press the 'SPACE' key as a response, or inhibit the key press, given the requirements of the experimental condition.
* In response to the keypress action from the participant, the post displays a green indicator and a tiny sound as a feedback for the correct action. Whereas, a red coloured indicator accompanied by a wrong answer buzzer sound in the background is displayed as a feedback for the incorrect action.
* Three different scences were created in Unity as per the task requirements for three experimental conditions.

### Hypovigilance Condition
* It is a low arousal experimental condition.
* In this experimental condition the sign posts are distributed in the frequencyrange of (90 -10), with 90% as the probability for the occurrences of the circle signsand 10% for the cross signs.
* To trigger a response from the participant, suppose the circle sign is encountered on the post, the participant must press the ‘SPACE’ key as a response, whereas incase the post displays a cross sign, the participant must inhibit the pressing of anykey.

### Hypervigilance or High Cognitive Load Condition
* To compare and contrast low state stimulation, a hypervigilance stimulation condtion was also incoroporated into the game as per CPT.
* In this experimental condition the sign posts were distributed in the frequency range of ()
* To make this task more difficult, the instructions were set as follows:
** Press space for first three circles and skip the fourth circle.
** Skip the first two crosses and press space for the third one.
** Repeat the same after every cycle
** Remember the instances as the distribution is very random.
** The cycle won't restart and keep going if the participant makes a mistake.

### Controlled Condition
* This condtion is the most low state stimulating condition.
* In this experimental condition, the participants are instructed to completely ignore the sign posts and keep driving till the very end.

## User Study
* Total of 23 participant were tested.
* The data collection was done using the EEG Nexus 4 device (2 main electrodes, 2 reference electrodes and 1 ground) from Mindmedia, Tobi Pro eye tracker for getting pupil size changes, also the reaction times from the participants were also considered.
* Further, an arousal scale with 0 - very low and 10 -very high was also presented to the participants at the end of every task.
* The lab room with ambient temperature with the 30 inches display monitor was set up. 
* Most of the experiments were done at the evening time.
* Every single experiment lasted for a minimum of 1 hrs and 30 mins, given the set up for the EEG and then the experiment process.
* Fp1 and Fp2 based on 10-20 positions were chosen as the location of interest.
* Biotrace+ software was used for EEG signal processing.
* Pyautogui was used to control the simultaniety of eeg recordings and the game tasks, as it was quite hard to integrate the Biotrace+ software with Unity using dll files.

## Data Analysis
* 
