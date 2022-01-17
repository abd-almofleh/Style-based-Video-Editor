This code is evaluated using CPU,
If you want to evaluate by using GPU only,
First you need to have CUDA installed on your device
Then
you can modify demoTalkNet.py and talkNet.py
file: modify all CPU into cuda . Then replace line 83 in talkNet.py into loadedState = torch.load(path)